using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CountryClubProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CountryClubProject.Controllers
{
    public class AccountController : Controller
    {

        //using Microsoft.AspNetCore.Identity  added to the top to make it easier to call.
        SignInManager<CountryClubUser> _signInManager;

        public AccountController(SignInManager<CountryClubUser> signInManager)
        {
            this._signInManager = signInManager;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        //Responds on GET /Account/Register
        public IActionResult Register()
        {
            return View();
        }
        //Responds on POST /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                
                CountryClubUser newUser = new CountryClubUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber

                };

                IdentityResult creationResult = this._signInManager.UserManager.CreateAsync(newUser).Result;
                if (creationResult.Succeeded)
                {
                    IdentityResult passwordResult = this._signInManager.UserManager.AddPasswordAsync(newUser, model.Password).Result;
                    if(passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(newUser, false);
                    }
                    //can now create a user and log in their info
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in creationResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View();
        }

        //Create the Views and any additional Models required for the functionality below:
        //Check the methods on UserManager and SignInManager to figure out how to do this
        //Beware of online posts..there will be a lot of renamed code within
        //Update your layout to display the  correct links depending on whether the user is logged in or not.( check sign out button @ property)
        //check newest web page posted for identity framework guidance on getting started

        public IActionResult SignOut()
        {
            this._signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignIn()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {

                CountryClubUser existingUser = this._signInManager.UserManager.FindByNameAsync(model.UserName).Result;
                if (existingUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult passwordResult = this._signInManager.CheckPasswordSignInAsync(existingUser, model.Password, false).Result;
                    if (passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("PasswordIncorrect", "Username or Password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserDoesNotExist", "Username or Password is incorrect.");

                }
            }
            return View();
        }

    }
}
