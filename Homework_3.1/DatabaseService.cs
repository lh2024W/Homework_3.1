using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3._1
{
    public class DatabaseService
    {
        public enum Menu { Register = 1, Login, Exit }
        DbContextOptions<ApplicationContext> options;
        public void EnsurePopulated()
        {

            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
               
                var userService = new UserRepository(db);
                int count = 1;
                foreach (var item in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{count++}. {item}");
                }
                int choice = 0;
                while (true)
                {
                    choice = Helper.GetInt("menu item");

                    switch ((Menu)choice)
                    {
                        case Menu.Register:
                            {
                                if (Helper.Check(userService))
                                {
                                    Helper.WriteSuccessfulMessage("Successful registration! Please log in.");
                                }
                                else
                                {
                                    goto case Menu.Register;
                                }
                                break;
                            }


                        case Menu.Login:
                            {
                                string authUsername = Helper.GetString("username");
                                string authPassword = Helper.GetString("password");
                                User user = new User
                                {
                                    UserName = authUsername,
                                    PasswordHash = authPassword
                                };
                                if (Helper.Check(user))
                                {
                                    if (userService.AuthenticateUser(user))
                                    {
                                        Helper.WriteSuccessfulMessage("Login successful. Redirecting to the main menu...");
                                    }
                                    else
                                    {
                                        goto case Menu.Login;
                                    }
                                }
                                break;
                            }

                        case Menu.Exit:
                            {
                                return;
                            }

                        default:
                            Helper.WriteErrorMessage("Invalid option. Try again.");
                            break;
                    }      
                }
            }
        }
    }
}
