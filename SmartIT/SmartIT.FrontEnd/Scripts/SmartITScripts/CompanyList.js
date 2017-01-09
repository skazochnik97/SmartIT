/// <reference path="../jquery-3.1.1.min.js" />
/// <reference path="../knockout-3.4.0.js" />


(function () {
    var viewModel = function () {
        var self = this;

        var IsUpdatable = false;

        self.Id = ko.observable(0);
        self.Name = ko.observable("");
        self.ShortName = ko.observable("");
        self.DateCreate = ko.observable("");
        self.DateCancel = ko.observable("");


        var CompanyInfo = {
            Id: self.Id,
            Name:self.Name,
            ShortName: self.ShortName,
            DateCreate: self.DateCreate,
            DateCancel: self.DateCancel,
        };

        self.CompanyList = ko.observable([]);


        loadInformation();

        function loadInformation() {

            $.ajax({
                url: "http://localhost:7056/api/Company",
                type: "GET"

            }).done(function (resp) {
                self.CompanyList(resp);
            });
        }

        self.getSelected = function (resp) {
            self.Id(resp.Id);
            self.Name(resp.Name);
            self.ShortName(resp.ShortName);
            self.DateCancel(resp.DateCancel);
            self.DateCreate(resp.DateCreate);
            IsUpdatable = true;
            $("#modalbox").modal("show");
        }

        self.save = function () {
            if (!IsUpdatable) {

                $.ajax({
                    url: "http://localhost:7056/api/Company",
                    type: "POST",
                    data: CompanyInfo,
                    datatype: "json",
                    contenttype: "application/json;utf-8",
                    error: function (jxqr, error, status) {
                        // парсинг json-объекта
                        var response = jQuery.parseJSON(jxqr.responseText);
                      
                      //  $('#modalbox').append("<h2>" + response['Message'] + "</h2>");
                        // добавляем ошибки свойства Year
                        if (response['ModelState']){
 
                            $.each(response['ModelState'], function (index, item) {
                                alert(item);
                            });
                        }
                   
                    }

                }).done(function (resp) {
                    self.Id(resp.Id);
                    $("#modalbox").modal("hide");
                    loadInformation();
                });
            } else {
                $.ajax({
                    url: "http://localhost:7056/api/Company/" + self.Id(),
                    type: "PUT",
                    data: CompanyInfo,
                    datatype: "json",
                    contenttype: "application/json;utf-8"
                }).done(function (resp) {
                    $("#modalbox").modal("hide");
                    loadInformation();
                    IsUpdatable = false;
                });

            }
        }

        self.delete = function (item) {
            $.ajax({
                url: "http://localhost:7056/api/Company/" + item.Id,
                type: "DELETE",
            }).done(function (resp) {
                loadInformation();
            });
        }

    };
    ko.applyBindings(new viewModel());
})();