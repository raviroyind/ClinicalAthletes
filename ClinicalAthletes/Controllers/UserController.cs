using ClinicalAthelete.Services;
using ClinicalAthletes.Entities;
using ClinicalAthletes.Models;
using ClinicalAthletes.Services;
using MvcApplication1.Models;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClinicalAthletes.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
          
         
        public ActionResult Dashboard()
        {
            return View(DataService.GetActiveExercisePlans());
        }

        public ActionResult Select(int id)
        {
            SelectViewModel selectViewModel = DataService.GetSelectView(id);
            return View(selectViewModel);
        }

        [HttpPost]
        public ActionResult Select(SelectViewModel selectViewModel)
        {
            List<ExerciseViewModel> list = DataService.GetExercises(25);

            string sJSONResponse = JsonConvert.SerializeObject(list);

            selectViewModel.UserExercisePlanSelection = new UserExercisePlanSelection
            {
                ExercisePlanId = selectViewModel.ExercisePlanId,
                UserId = HttpContext.User.Identity.Name
            };
             
            int userExercisePlanSelectionId =  DataService.InsertUserExercisePlanSelection(selectViewModel.UserExercisePlanSelection);

            return WeeklyPlan(userExercisePlanSelectionId);
        }

        public ActionResult WeeklyPlan(int userExercisePlanSelectionId)
        {
            WeeklyViewModel weeklyViewModel = new WeeklyViewModel(userExercisePlanSelectionId, User.Identity.Name);
             
            return View(weeklyViewModel);
        }

        public ActionResult Buy(int planId)
        {
            Session.Add("PLAN_ID", planId);
            return View(viewName: "~/Views/User/Buy.cshtml", model: planId.ToString());
        }
         
        public ActionResult Payment(string planId)
        {
            planId = Convert.ToString(Session["PLAN_ID"]);
           
            APIContext apiContext = Configuration.GetAPIContext();
            UserExercisePlanSelection userExercisePlanSelection = new UserExercisePlanSelection();

            if(!string.IsNullOrEmpty(planId))
                userExercisePlanSelection = DataService.GetUserExercisePlanSelection(Convert.ToInt32(planId), User.Identity.Name);
            else
            {
                throw new Exception("Exercise Plan Selection is empty.");
            }

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                { 
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/User/Payment?planId="+ planId+"&";
                     
                    var guid = Convert.ToString((new Random()).Next(100000));
                      
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, userExercisePlanSelection.ExercisePlanId);
                     
                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
 
                }
                else
                { 

                    var guid = Request.Params["guid"];
                    var paymentId= Request.Params["paymentId"]; 
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId);
                   
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                    else
                    {
                        string finalExcel = ExcelGenerator.GenerateExcel(User.Identity.Name,Convert.ToInt32(planId));
                         
                        DataService.InsertPurchases(new Purchase
                        {
                            Id = executedPayment.id,
                            UserId = User.Identity.Name,
                            UserExercisePlanSelectionId = userExercisePlanSelection.Id,
                            ExercisePlanId=userExercisePlanSelection.ExercisePlanId,
                            CartId=executedPayment.cart,
                            CreateTime=DateTime.Parse(executedPayment.create_time),
                            Intent=executedPayment.intent,
                            State=executedPayment.state,
                            Currency=executedPayment.transactions[0].amount.currency,
                            Amount=Convert.ToDouble(executedPayment.transactions[0].amount.total),
                            GenerateExcel = @"~/UserExcels/" +finalExcel
                        });

                        return View(viewName: "~/Views/User/PurchaseSuccess.cshtml", model: finalExcel);
                         
                    }
                }
            }
            catch (Exception ex)
            {

            }


            string fileName = Services.ExcelGenerator.GenerateExcel(User.Identity.Name, 104);
            return View(viewName: "~/Views/User/PurchaseSuccess.cshtml", model: fileName);
        }

        public ActionResult FailureView(string msg)
        {
            return View(viewName: "~/Views/User/FailureView.cshtml", model: msg);
        }
 
        private string GetBaseUrl()
        {
            return "/User/PaymentSuccessful";
        }

        public ActionResult PaymentCancelled()
        {
            // TODO: Handle cancelled payment
            return RedirectToAction("Error");
        }
 
        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl,int exercisePlanId)
        {
            ExercisePlan exercisePlan = DataService.GetExercisePlan(exercisePlanId);
            
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = @"Clinical Athletes-"+ exercisePlan.PlanName,
                currency = "USD",
                price = exercisePlan.Price.ToString(),
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = exercisePlan.Price.ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = exercisePlan.Price.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = @"Clinical Athletes-" + exercisePlan.PlanName,
                invoice_number = GetRandomInvoiceNumber(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);

        }

        private static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
        public ActionResult Account()
        {
            return View();
        }
    }
}
