using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using CountryClubProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CountryClubProject.Controllers
{
    public class ReceiptController : Controller
    {
        private CountryClubDbContext _countryClubDbContext;
        private EmailService _emailService;
        private SignInManager<CountryClubUser> _signInManager;
        private BraintreeGateway _brainTreeGateway;
        private SmartyStreets.USStreetApi.Client _usStreetApiClient;

        public ReceiptController(CountryClubDbContext countryClubDbContext, 
                                 EmailService emailService, 
                                 SignInManager<CountryClubUser> signInManager, 
                                 BraintreeGateway braintreeGateway, 
                                 SmartyStreets.USStreetApi.Client usStreetApiClient)
        {
            this._countryClubDbContext = countryClubDbContext;
            this._emailService = emailService;
            this._signInManager = signInManager;
            this._brainTreeGateway = braintreeGateway;
            this._usStreetApiClient = usStreetApiClient;
        }
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}