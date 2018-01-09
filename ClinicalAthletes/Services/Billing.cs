using PayPal.Api;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ClinicalAthletes.Services
{
    public static class Billing
    {
        public static Payment CreatePayment(string baseUrl, string intent)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Payment Resource
            var payment = new Payment()
            {
                intent = intent,    // `sale` or `authorize`
                payer = new Payer() { payment_method = "paypal",payer_info=new PayerInfo { billing_address=new Address
                {
                    line1= "1 Main St",
                    line2 = "SAN JOSE, CA 95131",
                    city= "SAN JOSE",
                    state= "CA",
                    postal_code= "95131",
                    country_code="US"
                },
                first_name="ravi",
                last_name="roy"
                } },
                transactions = GetTransactionsList(),
                redirect_urls = GetReturnUrls(baseUrl, intent)
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }

        private static List<Transaction> GetTransactionsList()
        {
            // A transaction defines the contract of a payment
            // what is the payment for and who is fulfilling it. 
            var transactionList = new List<Transaction>();
             
            // The Payment creation API requires a list of Transaction; 
            // add the created Transaction to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = "3.00",       // Total must be equal to sum of shipping, tax and subtotal.
                    details = new Details() // Details: Let's you specify details of a payment amount.
                    {
                        tax = "1",
                        shipping = "1",
                        subtotal = "1"
                    }
                } 
            });
            return transactionList;
        }

        private static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }

        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            var returnUrl = intent == "sale" ? "/User/PaymentSuccessful" : "/User/PaymentFail";

            // Redirect URLS
            // These URLs will determine how the user is redirected from PayPal 
            // once they have either approved or canceled the payment.
            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/User/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PayPalConfiguration.GetAPIContext();
             
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
    }
}