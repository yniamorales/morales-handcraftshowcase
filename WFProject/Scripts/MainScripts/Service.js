app.service("WFProjectSystemService", function ($http) {

    // Updated to accept 'data' to match his 'upsert' pattern
    this.UpsertService = function (data) {
        return $http({
            url: "/CraftSystem/SaveNewCraft",
            method: "POST",
            data: data,
            // These two lines are needed because you are uploading images
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    };

    this.LoginService = function (data) {
        return $http.post("/CraftSystem/CheckLogin", data);
    };

    this.getCraftsService = function () {
        return $http.get("/CraftSystem/GetMyCrafts");
    };
});