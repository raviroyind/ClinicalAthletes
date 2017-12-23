$(document).ready(function () {
    $("#ddlExerciseType").kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "Id",
        optionLabel: 'Select',
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/api/exercise/getExerciseTypes?exercisePlanId=" + $('#ExercisePlanId').val(),
                }
            }
        }
    });
});