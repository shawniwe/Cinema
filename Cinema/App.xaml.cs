using Cinema.Abstract;
using Cinema.Configuration;
using Cinema.DataAccessLayer;
using Cinema.Entities;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Tools.Mapper;
using CinemaNS.Abstract;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaNS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Configuration();
        }

        public void Configuration()
        {
            Mapper.Instance.CreateMapping<Movie, MovieModel>()
                .AddPropertyRelation(x => x.Minutes, x => x.Minutes)
                .AddPropertyRelation(x => x.Title, x => x.Title)
                .AddPropertyRelation(x => x.Sessions, x => x.Sessions)
                .AddPropertyRelation(x => x.Genre, x => x.Genre)
                .AddPropertyRelation(x => x.Country, x => x.Country)
                .AddPropertyRelation(x => x.Id, x => x.Id)
                .AddPropertyRelation(x => x.Photo, x => x.Photo)
                .AddPropertyRelation(x => x.MovieActors, x => x.MovieActors)
                .AddPropertyRelation(x => x.MovieProducers, x => x.MovieProducers)
                .AddPropertyRelation(x => x.MovieOperators, x => x.MovieOperators);

            Mapper.Instance.CreateMapping<CinemaNS.Entities.Place, PlaceModel>()
                .AddPropertyRelation(x => x.Number, x => x.Number)
                .AddPropertyRelation(x => x.Row, x => x.Row)
                .AddPropertyRelation(x => x.Id, x => x.Id);

            Mapper.Instance.CreateMapping<CinemaNS.Entities.Row, RowModel>()
                .AddPropertyRelation(x => x.Number, x => x.Number)
                .AddPropertyRelation(x => x.Hall, x => x.Hall)
                .AddPropertyRelation(x => x.Places, x => x.Places)
                .AddPropertyRelation(x => x.Id, x => x.Id);

            Mapper.Instance.CreateMapping<CinemaNS.Entities.Cinema, CinemaModel>()
                .AddPropertyRelation(x => x.IsActive, x => x.IsActive)
                .AddPropertyRelation(x => x.District, x => x.District)
                .AddPropertyRelation(x => x.Title, x => x.Title)
                .AddPropertyRelation(x => x.Halls, x => x.Halls)
                .AddPropertyRelation(x => x.Id, x => x.Id);

            Mapper.Instance.CreateMapping<CinemaNS.Entities.Hall, HallModel>()
                .AddPropertyRelation(x => x.Sessions, x => x.Sessions)
                .AddPropertyRelation(x => x.Cinema, x => x.Cinema)
                .AddPropertyRelation(x => x.Rows, x => x.Rows)
                .AddPropertyRelation(x => x.Id, x => x.Id)
                .AddPropertyRelation(x => x.Number, x => x.Number);

            Mapper.Instance.CreateMapping<CinemaNS.Entities.Session, SessionModel>()
                .AddPropertyRelation(x => x.StartDate, x => x.StartDate)
                .AddPropertyRelation(x => x.Cost, x => x.Cost)
                .AddPropertyRelation(x => x.Hall, x => x.Hall)
                .AddPropertyRelation(x => x.Movie, x => x.Movie)
                .AddPropertyRelation(x => x.Id, x => x.Id);

            Mapper.Instance.CreateMapping<Ticket, TicketModel>()
                .AddPropertyRelation(x => x.Session, x => x.Session)
                .AddPropertyRelation(x => x.Id, x => x.Id)
                .AddPropertyRelation(x => x.Place, x => x.Place);

            // filling em factory
            EntityManagersFactory.Instance.AddTransient<CinemaEntityManager>(nameof(Entities.Cinema));
            EntityManagersFactory.Instance.AddTransient<EntityManager<Country>>(nameof(Country));
            EntityManagersFactory.Instance.AddTransient<EntityManager<Person>>(nameof(Person));
            EntityManagersFactory.Instance.AddTransient<EntityManager<District>>(nameof(District));
            EntityManagersFactory.Instance.AddTransient<EntityManager<Genre>>(nameof(Genre));
            EntityManagersFactory.Instance.AddTransient<EntityManager<MovieActor>>(nameof(MovieActor));
            EntityManagersFactory.Instance.AddTransient<EntityManager<MovieOperator>>(nameof(MovieOperator));
            EntityManagersFactory.Instance.AddTransient<EntityManager<MovieProducer>>(nameof(MovieProducer));
            EntityManagersFactory.Instance.AddTransient<MovieEntityManager>(nameof(Movie));
            EntityManagersFactory.Instance.AddTransient<HallEntityManager>(nameof(Hall));
            EntityManagersFactory.Instance.AddTransient<SessionEntityManager>(nameof(Session));
            EntityManagersFactory.Instance.AddTransient<RowEntityManager>(nameof(Row));
            EntityManagersFactory.Instance.AddTransient<PlaceEntityManager>(nameof(Place));
            EntityManagersFactory.Instance.AddTransient<TicketEntityManager>(nameof(Ticket));

            // create default menu settings for the first start (seeding)
            var settings = ApplicationConfig.Instance.Settings;

            if (!settings.Where(x => x.SettingType == SettingType.MenuItem).Any())
            {
                var menuItems = new List<MenuItem>()
                {
                    new MenuItem { Key = "Movie", Title = "Фильмы" },
                    new MenuItem { Key = "Genre", Title = "Жанры" },
                    new MenuItem { Key = "Cinema", Title = "Кинотеатры" },
                    new MenuItem { Key = "Hall", Title = "Залы" },
                    new MenuItem { Key = "District", Title = "Районы кинотеатров" },
                    new MenuItem { Key = "Country", Title = "Страны" },
                    new MenuItem { Key = "Person", Title = "Участники кино" },
                    new MenuItem { Key = "Session", Title = "Сеансы" },
                    new MenuItem { Key = "Ticket", Title = "Билеты" },
                    new MenuItem { Key = "CinemaRepertoireReport", Title = "Отчет \"Репертуар кинотеатра\"" },
                    new MenuItem { Key = "RevenueReport", Title = "Отчет \"Выручка за период\"" },
                };

                var currentSettings = ApplicationConfig.Instance.Settings.ToList();
                foreach (var mi in menuItems)
                {
                    currentSettings.Add(new ApplicationSetting()
                    {
                        SettingType = SettingType.MenuItem,
                        Json = JsonConvert.SerializeObject(mi)
                    });
                }

                ApplicationConfig.Instance.Settings = currentSettings;
            }
        }
    }
}
