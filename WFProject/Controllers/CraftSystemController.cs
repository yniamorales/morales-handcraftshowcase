using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFProject.Models.Context;
using WFProject.Models.Tables;

namespace WFProject.Controllers
{
    public class CraftSystemController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult LogInPage()
        {
            return View();
        }

        public ActionResult AdminDB()
        {
            return View();
        }

        public ActionResult ArtisanDB()
        {
            return View();
        }

        public ActionResult Homepage()
        {
            return View();
        }

        public ActionResult AboutPage()
        {
            return View();
        }

        public ActionResult ArtisanPage()
        {
            return View();
        }

        public ActionResult AddProducts()
        {
            return View();
        }

        public ActionResult ArtisanProfile()
        {
            return View();
        }

        public string RegisterUser(string name, string username, string password, string user_role)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var existing = db.tbl_user.FirstOrDefault(u => u.username == username);
                    if (existing != null)
                    {
                        return "Username already taken!";
                    }

                    var newUser = new tbl_user
                    {
                        name = name,
                        username = username,
                        password = password,
                        user_role = user_role,
                        status = "active"
                    };
                    db.tbl_user.Add(newUser);
                    db.SaveChanges();

                    if (user_role == "artisan")
                    {
                        db.tbl_artisan.Add(new tbl_artisan
                        {
                            UserId = newUser.UserId,
                            artisanName = name,
                            artisanStatus = "active"
                        });
                        db.SaveChanges();
                    }

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string CheckLogin(string username, string password)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var user = db.tbl_user.FirstOrDefault(u => u.username == username && u.password == password);
                    if (user != null)
                    {
                        if (user.status != "active")
                        {
                            return "Account suspended!";
                        }

                        Session["UserID"] = user.UserId;
                        Session["UserName"] = user.name;
                        Session["UserRole"] = user.user_role;

                        return "Success|" + user.user_role.ToLower();
                    }
                    else
                    {
                        return "Invalid username or password!";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("LogInPage");
        }

        public JsonResult GetArtisanProfile()
        {
            if (Session["UserID"] == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            int uid = Convert.ToInt32(Session["UserID"]);
            using (var db = new CraftContext())
            {
                var profile = db.tbl_artisan.FirstOrDefault(a => a.UserId == uid);
                return Json(new
                {
                    success = true,
                    data = profile ?? new tbl_artisan { artisanName = Session["UserName"].ToString() }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public string SaveArtisanProfile(int ArtisanId, string artisanName, string contactNum, string artisanBio)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var existing = db.tbl_artisan.Find(ArtisanId);
                    if (existing != null)
                    {
                        existing.artisanName = artisanName;
                        existing.contactNum = contactNum;
                        existing.artisanBio = artisanBio;
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "Profile not found!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpPost]
        public string SaveNewCraft(string ProductName, string ProductDesc, HttpPostedFileBase imageFile)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return "Session expired!";
                }

                int uid = Convert.ToInt32(Session["UserID"]);
                string relativePath = "/Content/ProductImages/default.png";

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    string path = Server.MapPath("~/Content/ProductImages/") + fileName;
                    imageFile.SaveAs(path);
                    relativePath = "/Content/ProductImages/" + fileName;
                }

                using (var db = new CraftContext())
                {
                    var artisan = db.tbl_artisan.FirstOrDefault(a => a.UserId == uid);
                    if (artisan == null)
                    {
                        return "Artisan profile missing!";
                    }

                    var craft = new tbl_product
                    {
                        ProductName = ProductName,
                        ProductDesc = ProductDesc,
                        ProductImgUrl = relativePath,
                        ProductStatus = "pending",
                        ArtisanId = artisan.ArtisanId
                    };

                    db.tbl_product.Add(craft);
                    db.SaveChanges();
                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeleteCraft(int id)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var craft = db.tbl_product.Find(id);
                    if (craft != null)
                    {
                        db.tbl_product.Remove(craft);
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "Product not found!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpPost]
        public string UpdateCraftFull(int ProductId, string ProductName, string ProductDesc, HttpPostedFileBase imageFile)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var craft = db.tbl_product.Find(ProductId);
                    if (craft == null)
                    {
                        return "Product not found!";
                    }

                    craft.ProductName = ProductName;
                    craft.ProductDesc = ProductDesc;

                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                        string path = Server.MapPath("~/Content/ProductImages/") + fileName;
                        imageFile.SaveAs(path);
                        craft.ProductImgUrl = "/Content/ProductImages/" + fileName;
                    }

                    db.SaveChanges();
                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateCraftName(int id, string newName)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var craft = db.tbl_product.Find(id);
                    if (craft != null)
                    {
                        craft.ProductName = newName;
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "Product not found!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public JsonResult GetArtisanCrafts()
        {
            int uid = Convert.ToInt32(Session["UserID"]);
            using (var db = new CraftContext())
            {
                var artisan = db.tbl_artisan.FirstOrDefault(a => a.UserId == uid);
                var crafts = artisan != null
                    ? db.tbl_product.Where(p => p.ArtisanId == artisan.ArtisanId).ToList()
                    : new List<tbl_product>();
                return Json(new { success = true, data = crafts }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetShowcaseData()
        {
            using (var db = new CraftContext())
            {
                var data = db.tbl_artisan.Select(a => new
                {
                    a.artisanName,
                    Crafts = db.tbl_product
                        .Where(p => p.ArtisanId == a.ArtisanId && p.ProductStatus == "approved")
                        .Select(p => new {
                            p.ProductName,
                            p.ProductDesc,
                            p.ProductImgUrl
                        }).ToList()
                })
                .Where(a => a.Crafts.Count > 0)
                .ToList();

                return Json(new { success = true, artisans = data }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetAdminData()
        {
            using (var db = new CraftContext())
            {
                var artisans = (from a in db.tbl_artisan
                                join u in db.tbl_user on a.UserId equals u.UserId
                                select new
                                {
                                    a.ArtisanId,
                                    u.username,
                                    a.artisanName,
                                    a.artisanStatus
                                }).ToList();

                var users = db.tbl_user.Where(u => u.user_role == "user").Select(u => new {
                    u.UserId,
                    u.name,
                    u.username,
                    u.user_role,
                    u.status
                }).ToList();

                var products = (from p in db.tbl_product
                                join a in db.tbl_artisan on p.ArtisanId equals a.ArtisanId
                                join u in db.tbl_user on a.UserId equals u.UserId
                                select new
                                {
                                    p.ProductId,
                                    p.ProductName,
                                    p.ProductDesc,
                                    p.ProductImgUrl,
                                    p.ProductStatus,
                                    p.ArtisanId,
                                    username = u.username
                                }).ToList();

                return Json(new
                {
                    success = true,
                    artisans = artisans,
                    users = users,
                    products = products,
                    totalProducts = products.Count,
                    approvedCount = products.Count(p => p.ProductStatus == "approved"),
                    pendingCount = products.Count(p => p.ProductStatus == "pending"),
                    userCount = users.Count,
                    artisanCount = artisans.Count
                }, JsonRequestBehavior.AllowGet);
            }
        }






        public string ApproveProduct(int id)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var product = db.tbl_product.Find(id);
                    if (product != null)
                    {
                        product.ProductStatus = "approved";
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "Product not found!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [HttpPost]
        public string ToggleArtisanStatus(int id)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var artisan = db.tbl_artisan.Find(id);
                    if (artisan != null)
                    {
                        artisan.artisanStatus = (artisan.artisanStatus == "active") ? "banned" : "active";
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "Artisan not found!";
                }
            }
            catch (Exception ex) { return "Error: " + ex.Message; }
        }

        public string ToggleUserStatus(int id)
        {
            try
            {
                using (var db = new CraftContext())
                {
                    var user = db.tbl_user.Find(id);
                    if (user != null)
                    {
                        user.status = (user.status == "active") ? "disabled" : "active";
                        db.SaveChanges();
                        return "Success!";
                    }
                    return "User not found!";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
