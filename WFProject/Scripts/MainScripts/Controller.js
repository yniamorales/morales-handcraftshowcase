app.controller("CraftSystem", function ($scope, $http) {

    $scope.submitted = false;
    $scope.artisanProfile = {};
    $scope.craftsInfo = [];
    $scope.showcaseData = [];

    //login
    $scope.loginFunc = function () {
        $scope.submitted = true;
        if ($scope.loginForm.$invalid) return;

        $http.post('/CraftSystem/CheckLogin', {
            username: $scope.loginUser,
            password: $scope.loginPassword
        }).then(function (response) {
            let res = response.data.toString().trim();
            if (res.startsWith("Success|")) {
                let role = res.split("|")[1];
                Swal.fire({
                    icon: 'success',
                    title: 'Welcome!',
                    timer: 1500,
                    showConfirmButton: false
                }).then(() => {
                    if (role === "artisan") window.location.href = "/CraftSystem/ArtisanDB";
                    else if (role === "admin") window.location.href = "/CraftSystem/AdminDB";
                    else window.location.href = "/CraftSystem/Homepage";
                });
            } else {
                Swal.fire("Error", res, "error");
            }
        });
    };

    //registration
    $scope.saveFunc = function () {
        $scope.submitted = true;
        if ($scope.registerForm.$invalid) return;

        let data = $.param({
            name: $scope.Name,
            username: $scope.UserName,
            password: $scope.Password,
            user_role: $scope.RegisterAs
        });

        $http.post('/CraftSystem/RegisterUser', data, {
            headers: { "Content-Type": "application/x-www-form-urlencoded" }
        }).then(function (response) {
            let res = response.data.toString().trim();
            if (res === "Success!") {
                Swal.fire({ icon: "success", title: "Registration Successful!" })
                    .then(() => { window.location.href = "/CraftSystem/LogInPage"; });
            } else {
                Swal.fire("Error", res, "error");
            }
        });
    };

    //load profile
    $scope.loadProfile = function () {
        $http.get('/CraftSystem/GetArtisanProfile').then(function (response) {
            if (response.data.success) {
                $scope.artisanProfile = response.data.data;
                setTimeout(() => M.updateTextFields(), 100);
            }
        });
    };

    //update artisan
    $scope.updateArtisanProfile = function () {
        if (!$scope.artisanProfile || !$scope.artisanProfile.ArtisanId) {
            console.error("Missing Profile Data:", $scope.artisanProfile);
            Swal.fire("Error", "Profile data is not fully synced. Please refresh.", "warning");
            return;
        }

        let payload = {
            ArtisanId: parseInt($scope.artisanProfile.ArtisanId),
            artisanName: $scope.artisanProfile.artisanName,
            contactNum: $scope.artisanProfile.contactNum,
            artisanBio: $scope.artisanProfile.artisanBio
        };

        let data = $.param(payload);

        $http.post('/CraftSystem/SaveArtisanProfile', data, {
            headers: { "Content-Type": "application/x-www-form-urlencoded" }
        }).then(function (response) {
            let res = response.data.toString().trim();
            if (res === "Success!") {
                Swal.fire({
                    icon: 'success',
                    title: 'Saved!',
                    text: 'Profile updated successfully.',
                    confirmButtonColor: '#f50057'
                });
                $scope.loadProfile();
            } else {
                Swal.fire("Error", res, "error");
            }
        });
    };

    $scope.loadCrafts = function () {
        $http.get('/CraftSystem/GetArtisanCrafts').then(function (response) {
            if (response.data.success) { $scope.craftsInfo = response.data.data; }
        });
    };

    //remove product/craft
    $scope.removeCraft = function (id) {
        Swal.fire({
            title: 'Delete this craft?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#f50057'
        }).then((result) => {
            if (!result.isConfirmed) return;
            let data = $.param({ id: id });
            $http.post('/CraftSystem/DeleteCraft', data, {
                headers: { "Content-Type": "application/x-www-form-urlencoded" }
            }).then(function (response) {
                if (response.data.toString().trim() === "Success!") {
                    Swal.fire("Deleted!", "Craft removed.", "success");
                    $scope.loadCrafts();
                }
            });
        });
    };

    //edit product
    $scope.editCraft = function (craft) {
        Swal.fire({
            title: 'Edit Craft',
            html: `<input id="swal-input1" class="swal2-input" value="${craft.ProductName}">
                   <textarea id="swal-input2" class="swal2-textarea" style="height:150px;">${craft.ProductDesc}</textarea>
                   <input id="swal-input3" type="file" accept="image/*" class="swal2-file">`,
            showCancelButton: true,
            preConfirm: () => ({
                name: document.getElementById('swal-input1').value,
                desc: document.getElementById('swal-input2').value,
                file: document.getElementById('swal-input3').files[0]
            })
        }).then((result) => {
            if (!result.isConfirmed) return;
            var fd = new FormData();
            fd.append("ProductId", craft.ProductId);
            fd.append("ProductName", result.value.name);
            fd.append("ProductDesc", result.value.desc);
            if (result.value.file) fd.append("imageFile", result.value.file);

            $http.post('/CraftSystem/UpdateCraftFull', fd, {
                transformRequest: angular.identity,
                headers: { "Content-Type": undefined }
            }).then(function (response) {
                if (response.data.toString().trim() === "Success!") {
                    Swal.fire("Updated!", "Craft updated.", "success");
                    $scope.loadCrafts();
                }
            });
        });
    };


    //submit product
    $scope.newCraft = {};
    $scope.submitCraft = function () {
        if (!$scope.newCraft.ProductName || !$scope.newCraft.ProductDesc) {
            Swal.fire("Warning", "Please enter a name and description.", "warning");
            return;
        }

        if (!$scope.artisanProfile || !$scope.artisanProfile.ArtisanId) {
            Swal.fire("Error", "Artisan profile not loaded. Please refresh.", "error");
            return;
        }

        var fd = new FormData();
        fd.append("ProductName", $scope.newCraft.ProductName);
        fd.append("ProductDesc", $scope.newCraft.ProductDesc);


        fd.append("ArtisanId", $scope.artisanProfile.ArtisanId);

        var file = document.getElementById("imageUpload").files[0];
        if (file) fd.append("imageFile", file);

        $http.post('/CraftSystem/SaveNewCraft', fd, {
            transformRequest: angular.identity,
            headers: { "Content-Type": undefined }
        }).then(function (response) {
            let res = response.data.toString().trim();

            if (res === "Success!") {
                Swal.fire({
                    icon: "success",
                    title: "Published!",
                    text: "Your craft is now pending approval.",
                    confirmButtonColor: '#f50057'
                }).then(() => {
                    window.location.href = "/CraftSystem/ArtisanDB";
                });
            } else {
                Swal.fire("Error", res, "error");
            }
        });
    };

    //show artisan product
    $scope.initShowcase = function () {
        $http.get('/CraftSystem/GetShowcaseData').then(function (response) {
            if (response.data.success) { $scope.showcaseData = response.data.artisans; }
        });
    };

    $scope.openExhibit = function (title, img, desc) {
        document.getElementById("modalTitle").innerText = title;
        document.getElementById("modalImg").src = img;
        document.getElementById("modalDesc").innerText = desc;
        document.getElementById("exhibitModal").style.display = "flex";
        document.body.style.overflow = "hidden";
    };

    $scope.closeExhibit = function () {
        document.getElementById("exhibitModal").style.display = "none";
        document.body.style.overflow = "auto";
    };

    const path = window.location.href;
    if (path.includes("ArtisanDB") || path.includes("ArtisanProfile")) {
        $scope.loadProfile();
        if (path.includes("ArtisanDB")) $scope.loadCrafts();
    }
    if (path.includes("ArtisanPage")) $scope.initShowcase();
});


app.controller("AdminCtrl", function ($scope, $http) {
    $scope.init = function () {
        $http.get("/CraftSystem/GetAdminData").then(function (response) {
            if (response.data.success) {
                $scope.artisans = response.data.artisans;
                $scope.products = response.data.products;
                $scope.users = response.data.users;

                $scope.totalProducts = parseInt(response.data.totalProducts);
                $scope.approvedCount = parseInt(response.data.approvedCount);
                $scope.pendingCount = parseInt(response.data.pendingCount);
                $scope.userCount = parseInt(response.data.userCount);
                $scope.artisanCount = parseInt(response.data.artisanCount);

                setTimeout(() => $scope.renderCharts(), 200);
            }
        });
    };

    //create chart
    $scope.renderCharts = function () {
        ["chartProducts", "chartUsersArtisans", "chartProductsPerArtisan"].forEach(id => {
            let chartStatus = Chart.getChart(id);
            if (chartStatus) chartStatus.destroy();
        });

        new Chart(document.getElementById("chartProducts"), {
            type: "doughnut",
            data: {
                labels: ["Approved", "Pending"],
                datasets: [{
                    data: [parseInt($scope.approvedCount), parseInt($scope.pendingCount)],
                    backgroundColor: ["#f50057", "#ff80ab"]
                }]
            },
            options: { responsive: true, maintainAspectRatio: false }
        });


        new Chart(document.getElementById("chartUsersArtisans"), {
            type: "pie",
            data: {
                labels: ["Users", "Artisans"],
                datasets: [{
                    data: [parseInt($scope.userCount), parseInt($scope.artisanCount)],
                    backgroundColor: ["#d81b60", "#f48fb1"]
                }]
            },
            options: { responsive: true, maintainAspectRatio: false }
        });


        let artisanNames = $scope.artisans.map(a => a.artisanName);
        let productCounts = $scope.artisans.map(art =>
            $scope.products.filter(p => p.ArtisanId == art.ArtisanId).length
        );

        new Chart(document.getElementById("chartProductsPerArtisan"), {
            type: "bar",
            data: {
                labels: artisanNames,
                datasets: [{
                    label: "Products",
                    data: productCounts,
                    backgroundColor: "#ad1457"
                }]
            },
            options: { responsive: true, maintainAspectRatio: false }
        });
    };

    //disable artisan shop
    $scope.disableArtisan = function (id) {
        let data = $.param({ id: id });
        $http.post('/CraftSystem/ToggleArtisanStatus', data, {
            headers: { "Content-Type": "application/x-www-form-urlencoded" }
        }).then(function (response) {
            if (response.data.toString().trim() === "Success!") {
                Swal.fire("Success", "Artisan status updated.", "success");
                $scope.init();
            } else {
                Swal.fire("Error", response.data, "error");
            }
        });
    };

    //enable and disable user (cannot log in when banned)
    $scope.toggleUser = function (id) {
        let data = $.param({ id: id });
        $http.post('/CraftSystem/ToggleUserStatus', data, {
            headers: { "Content-Type": "application/x-www-form-urlencoded" }
        }).then(function (response) {
            if (response.data.toString().trim() === "Success!") {
                Swal.fire("Success", "User status updated.", "success");
                $scope.init();
            } else {
                Swal.fire("Error", response.data, "error");
            }
        });
    };
    $scope.init();
});