﻿using System;
using System.Linq;
using MedicalJournals.Enums;
using MedicalJournals.Helpers;
using MedicalJournals.Identity;
using MedicalJournals.Models.Data;
using MedicalJournals.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MedicalJournals.Entities.Extensions
{
    /// <summary>
    /// Object used to seed an empty database
    /// </summary>
    public static class SeedDataExtensions
    {
        private static ILogger _logger;
        private static JournalContext _context;
        private static bool _isInMemoryDatabase;
        private static UserManager<ApplicationUser> _userManager;
        private static RoleManager<JournalRole> _roleManager;
        private const string DefaultUserName = "ossent@yahoo.co.uk";
        private const string DefaultPublisher = "The American Orthopaedic Society for Sports Medicine";


        public static void SeedData(this IServiceScopeFactory scopeFactory, ILogger logger)
        {
            _logger = logger;

            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<JournalContext>();
                _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<JournalRole>>();

                _logger.LogDebug("Starting database table data seeding.");

                _context = context;

                if(_context==null || _userManager==null)
                    throw new ArgumentNullException("Database or identity objects are not set");

                if (_context.AllMigrationsApplied())
                {
                    InitializeIdentity();
                    ApplyData(false);
                }
                else
                {
                    _logger.LogDebug("Database migrations are not correctly applied.");

                }
            }
        }

        public static void SeedData(JournalContext context, ILogger logger)
        {

            if (_context == null)
                throw new ArgumentNullException("Database context is not set");

            _logger = logger;
            _context = context;
            ApplyData(true);
        }

        private static void ApplyData(bool isInMemoryDatabase)
        {
            _isInMemoryDatabase = isInMemoryDatabase;

            if (!_isInMemoryDatabase)
                _context.Database.OpenConnection();

            AddCategories();
            AddCountries();
            AddPublishers();
            AddJournals();


        }

        private static void AddCountries()
        {
            if (!_context.Countries.Any())
            {
                SwitchIdentity("Countries", true, _isInMemoryDatabase);

                _context.Countries.AddRange(
                    new Country {CountryId = 1, CountryName = "Algeria", CountryCode = "AL", IsEnabled = false},
                    new Country {CountryId = 2, CountryName = "Angola", CountryCode = "AN", IsEnabled = false},
                    new Country {CountryId = 3, CountryName = "Benin", CountryCode = "BE", IsEnabled = false},
                    new Country {CountryId = 4, CountryName = "Botswana", CountryCode = "BO", IsEnabled = false},
                    new Country {CountryId = 5, CountryName = "Burkina Faso", CountryCode = "BU", IsEnabled = false},
                    new Country {CountryId = 6, CountryName = "Burundi", CountryCode = "BR", IsEnabled = false},
                    new Country {CountryId = 7, CountryName = "Cameroon", CountryCode = "CM", IsEnabled = false},
                    new Country {CountryId = 8, CountryName = "Cape Verde", CountryCode = "CV", IsEnabled = false},
                    new Country {CountryId = 9, CountryName = "Central African Republic", CountryCode = "CR", IsEnabled = false},
                    new Country {CountryId = 10, CountryName = "Chad", CountryCode = "CD", IsEnabled = false},
                    new Country {CountryId = 11, CountryName = "Comoros", CountryCode = "CO", IsEnabled = false},
                    new Country {CountryId = 12, CountryName = "Côte d'Ivoire", CountryCode = "CI", IsEnabled = false},
                    new Country {CountryId = 13, CountryName = "Democratic Republic of the Congo", CountryCode = "DC", IsEnabled = false},
                    new Country {CountryId = 14, CountryName = "Djibouti", CountryCode = "DJ", IsEnabled = false},
                    new Country {CountryId = 15, CountryName = "Egypt", CountryCode = "EG", IsEnabled = false},
                    new Country {CountryId = 16, CountryName = "Equatorial Guinea", CountryCode = "EQ", IsEnabled = false},
                    new Country {CountryId = 17, CountryName = "Eritrea", CountryCode = "ER", IsEnabled = false},
                    new Country {CountryId = 18, CountryName = "Ethiopia", CountryCode = "ET", IsEnabled = false},
                    new Country {CountryId = 19, CountryName = "Gabon", CountryCode = "GA", IsEnabled = false},
                    new Country {CountryId = 20, CountryName = "Gambia", CountryCode = "GM", IsEnabled = false},
                    new Country {CountryId = 21, CountryName = "Ghana", CountryCode = "GH", IsEnabled = false},
                    new Country {CountryId = 22, CountryName = "Guinea", CountryCode = "GU", IsEnabled = false},
                    new Country {CountryId = 23, CountryName = "Guinea-Bissau", CountryCode = "GI", IsEnabled = false},
                    new Country {CountryId = 24, CountryName = "Kenya", CountryCode = "KE", IsEnabled = true},
                    new Country {CountryId = 25, CountryName = "Lesotho", CountryCode = "LE", IsEnabled = false},
                    new Country {CountryId = 26, CountryName = "Liberia", CountryCode = "LI", IsEnabled = false},
                    new Country {CountryId = 27, CountryName = "Libya", CountryCode = "LB", IsEnabled = false},
                    new Country {CountryId = 28, CountryName = "Madagascar", CountryCode = "MD", IsEnabled = false},
                    new Country {CountryId = 29, CountryName = "Malawi", CountryCode = "ML", IsEnabled = false},
                    new Country {CountryId = 30, CountryName = "Mali", CountryCode = "MA", IsEnabled = false},
                    new Country {CountryId = 31, CountryName = "Mauritania", CountryCode = "MU", IsEnabled = false},
                    new Country {CountryId = 32, CountryName = "Mauritius", CountryCode = "MT", IsEnabled = false},
                    new Country {CountryId = 33, CountryName = "Morocco", CountryCode = "MO", IsEnabled = false},
                    new Country {CountryId = 34, CountryName = "Mozambique", CountryCode = "MZ", IsEnabled = false},
                    new Country {CountryId = 35, CountryName = "Namibia", CountryCode = "NB", IsEnabled = false},
                    new Country {CountryId = 36, CountryName = "Niger", CountryCode = "NR", IsEnabled = false},
                    new Country {CountryId = 37, CountryName = "Nigeria", CountryCode = "NI", IsEnabled = false},
                    new Country {CountryId = 38, CountryName = "Republic of the Congo", CountryCode = "RC", IsEnabled = false},
                    new Country {CountryId = 39, CountryName = "Réunion", CountryCode = "RE", IsEnabled = false},
                    new Country {CountryId = 40, CountryName = "Rwanda", CountryCode = "RW", IsEnabled = false},
                    new Country {CountryId = 41, CountryName = "São Tomé and Príncipe", CountryCode = "ST", IsEnabled = false},
                    new Country {CountryId = 42, CountryName = "Senegal", CountryCode = "SE", IsEnabled = false},
                    new Country {CountryId = 43, CountryName = "Seychelles", CountryCode = "SY", IsEnabled = false},
                    new Country {CountryId = 44, CountryName = "Sierra Leone", CountryCode = "SL", IsEnabled = false},
                    new Country {CountryId = 45, CountryName = "Somalia", CountryCode = "SO", IsEnabled = false},
                    new Country {CountryId = 46, CountryName = "South Africa", CountryCode = "ZA", IsEnabled = false},
                    new Country {CountryId = 47, CountryName = "Sudan", CountryCode = "SU", IsEnabled = false},
                    new Country {CountryId = 48, CountryName = "South Sudan", CountryCode = "SS", IsEnabled = false},
                    new Country {CountryId = 49, CountryName = "Swaziland", CountryCode = "SZ", IsEnabled = false},
                    new Country {CountryId = 50, CountryName = "Tanzania", CountryCode = "TZ", IsEnabled = false},
                    new Country {CountryId = 51, CountryName = "Togo", CountryCode = "TG", IsEnabled = false},
                    new Country {CountryId = 52, CountryName = "Tunisia", CountryCode = "TU", IsEnabled = false},
                    new Country {CountryId = 53, CountryName = "Uganda", CountryCode = "UG", IsEnabled = true},
                    new Country {CountryId = 54, CountryName = "Western Sahara", CountryCode = "WS", IsEnabled = false},
                    new Country {CountryId = 55, CountryName = "Zambia", CountryCode = "ZA", IsEnabled = false},
                    new Country {CountryId = 56, CountryName = "Zimbabwe", CountryCode = "ZI", IsEnabled = false},
                    new Country {CountryId = 57, CountryName = "Unknown", CountryCode = "UU", IsEnabled = true},
                    new Country {CountryId = 58, CountryName = "United States", CountryCode = "US", IsEnabled = true },
                    new Country {CountryId = 59, CountryName = "United Kingdom", CountryCode = "UK", IsEnabled = true }
                );

                _context.SaveChanges();
                SwitchIdentity("Countries", false, _isInMemoryDatabase);
                _logger.LogDebug("Completed Countries table updates with {0} rows.", _context.Countries.Count());
            }
        }

        private static void AddCategories()
        {
            if (!_context.Categories.Any())
            {
                SwitchIdentity("Categories", true, _isInMemoryDatabase);

                _context.Categories.AddRange(
                    new Category {CategoryId = 1, CategoryName = "Alternative and traditional"},
                    new Category {CategoryId = 2, CategoryName = "Anatomy"},
                    new Category {CategoryId = 3, CategoryName = "Anesthesiology and palliative medicine"},
                    new Category {CategoryId = 4, CategoryName = "Behavioral"},
                    new Category {CategoryId = 5, CategoryName = "Cardiology"},
                    new Category {CategoryId = 6, CategoryName = "Dentistry"},
                    new Category {CategoryId = 7, CategoryName = "Dermatology"},
                    new Category {CategoryId = 8, CategoryName = "Epidemiology"},
                    new Category {CategoryId = 9, CategoryName = "Gastroenterology and hepatology"},
                    new Category {CategoryId = 10, CategoryName = "Hematology"},
                    new Category {CategoryId = 11, CategoryName = "Immunology"},
                    new Category {CategoryId = 12, CategoryName = "Laboratory"},
                    new Category {CategoryId = 13, CategoryName = "Microbiology"},
                    new Category {CategoryId = 14, CategoryName = "Neurology"},
                    new Category {CategoryId = 15, CategoryName = "Nutrition"},
                    new Category {CategoryId = 16, CategoryName = "Oncology"},
                    new Category {CategoryId = 17, CategoryName = "Pediatrics"},
                    new Category {CategoryId = 18, CategoryName = "Psychiatry"},
                    new Category {CategoryId = 19, CategoryName = "Rheumatology"},
                    new Category {CategoryId = 20, CategoryName = "Sports medicine"},
                    new Category {CategoryId = 21, CategoryName = "Toxicology"},
                    new Category {CategoryId = 22, CategoryName = "Urology"},
                    new Category {CategoryId = 23, CategoryName = "Veterinary"}
                );

                _context.SaveChanges();
                _logger.LogDebug("Completed Categories table updates with {0} rows.", _context.Categories.Count());
                SwitchIdentity("Categories", false, _isInMemoryDatabase);
            }
        }

        private static void AddPublishers()
        {
            if (!_context.Publishers.Any())
            {
                var user = _userManager.FindByNameAsync(DefaultUserName).Result;
                var country = _context.Countries.FirstOrDefault(x => x.CountryCode == "US");

                if (user != null)
                {

                    _context.Publishers.AddRange(
                        new Publisher
                        {
                            Name = DefaultPublisher,
                            IsEnabled = true,
                            User = user,       
                            Country = country
                        }
                    );

                    _context.SaveChanges();
                    _logger.LogDebug("Completed Publishers table updates with {0} rows.", _context.Categories.Count());
                }

            }
        }

        private static void AddJournals()
        {
            if (!_context.Journals.Any())
            {
                var publisher = _context.Publishers.FirstOrDefault(x=>x.Name== DefaultPublisher);

                if (publisher != null)
                {
                    _context.Journals.AddRange(
                        new Journal
                        {
                            Title = "The American Orthopaedic Society for Sports Medicine",
                            IsEnabled = true,
                            Publisher = publisher,
                            Description = "American Journal of Sports Medicine is a peer-reviewed academic journal that publishes papers in the field of Sport Sciences. " +
                                          "The journal's editor is Bruce Reider, MD. " +
                                          "It has been in publication since 1972 and is currently published by " +
                                          "SAGE Publications in association with American Orthopedic Society for Sports Medicine",
                            CategoryId = 20
                        }
                    );

                    _context.SaveChanges();
                    _logger.LogDebug("Completed Journals table updates with {0} rows.", _context.Categories.Count());
                }
            }
        }

        //Create User=Admin@Admin.com with password=123456 in the Admin role        
        public static void InitializeIdentity()
        {

            const string password = "123456aZ.";

            //Create Role Admin if it does not exist
            var adminRoleName = Roles.Admin.ToDescription();
            var adminRole = _roleManager.FindByNameAsync(adminRoleName).Result;
            if (adminRole == null)
            {
                adminRole = new JournalRole(adminRoleName);

                var roleresult = _roleManager.CreateAsync(adminRole).Result;
                if (!roleresult.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user: {0}.", roleresult.Errors.FirstOrDefault());
                    return;
                }

                _logger.LogDebug("Created role {0}.", adminRoleName);
            }

            var publisherRoleName = Roles.Publisher.ToDescription();
            var publisherRole = _roleManager.FindByNameAsync(publisherRoleName).Result;
            if (publisherRole == null)
            {
                publisherRole = new JournalRole(publisherRoleName);

                var roleresult = _roleManager.CreateAsync(publisherRole).Result;
                if (!roleresult.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user: {0}.", roleresult.Errors.FirstOrDefault());
                    return;
                }

                _logger.LogDebug("Created role {0}.", publisherRoleName);
            }

            var publicRoleName = Roles.Public.ToDescription();
            var publicRole = _roleManager.FindByNameAsync(publicRoleName).Result;
            if (publicRole == null)
            {
                publicRole = new JournalRole(publicRoleName);

                var roleresult = _roleManager.CreateAsync(publicRole).Result;
                if (!roleresult.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user: {0}.", roleresult.Errors.FirstOrDefault());
                    return;
                }

                _logger.LogDebug("Created role {0}.", publicRoleName);
            }

            // add the admin role to the default user
            var user = _userManager.FindByNameAsync(DefaultUserName).Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = DefaultUserName,
                    Email = DefaultUserName,
                    FirstName = "Oscar",
                    LastName = "Ssentoogo",
                    Created = DateTime.UtcNow,
                    EmailConfirmed = true
                };
            
                var result = _userManager.CreateAsync(user, password).Result;
                if (!result.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user: {0}.", result.Errors.FirstOrDefault());
                    return;
                }
                _logger.LogDebug("Added default user. Succeeded is {0}.", result.Succeeded);
            }
            else
            {
                ////    Logger<>.LogFormat(LogType.Debug, "Default user already exists");
                user = _userManager.FindByNameAsync(DefaultUserName).Result;
            }


            // Add user to Role Admin if not already added                
            if (!_userManager.IsInRoleAsync(user, adminRoleName).Result)
            {
                var result = _userManager.AddToRoleAsync(user, adminRoleName).Result;
                if (!result.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user to admin role: {0}.", result.Errors.FirstOrDefault());
                }
            }

            // Add user to Role publisher if not already added                
            if (!_userManager.IsInRoleAsync(user, publisherRoleName).Result)
            {
                var result = _userManager.AddToRoleAsync(user, publisherRoleName).Result;
                if (!result.Succeeded)
                {
                    _logger.LogError("Error when trying to add default user to publisher role: {0}.", result.Errors.FirstOrDefault());
                }
            }
        }

        private static void SwitchIdentity(string table, bool isOn, bool isInMemoryDatabase)
        {
            if(!isInMemoryDatabase)
                _context.Database.ExecuteSqlCommand(isOn
                    ? $@"SET IDENTITY_INSERT [dbo].[{table}] ON"
                    : $@"SET IDENTITY_INSERT [dbo].[{table}] OFF");
        }
    }
}
