using Canteen.Core.BusinessModels;
using Canteen.Core.DataAccess;
using Canteen.Core.Entities;
using Canteen.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Core.Managers
{
    public class RestaurantManager:IRestaurantManager
    {
        private readonly ILogger<IRestaurantManager> _log;
        private readonly IUnitOfWork _uow;
        private readonly Jwt _secure;
        public RestaurantManager(ILogger<IRestaurantManager> log, IOptions<Jwt> secure, IUnitOfWork uow)
        {
            _log = log;
            _uow = uow;
            _secure = secure.Value;
        }

        public async Task<RestaurantModel> CreateRestaurant(RestaurantModel model)
        {
            try
            {
               var restrt = _uow.GetRepository<Restaurant>().FindByCondition(r => r.Name == model.Name);
                // We want to create a new Restaurant 
                if (restrt ==null) return null;
                var rest = new RestaurantModel().Create(model);

               
                rest.LogoName = model.Logo;
                _uow.GetRepository<Restaurant>().Insert(rest);
                _uow.Commit();
                return model;

            }
            catch(NullReferenceException ex)
            {
                _log.LogError($"An error occured creating a restaurant \n{ex.Message}", ex.Message);
                return null;
            }
        }

        public Restaurant LoadOneRestaurant(long id)
        {
            try
            {
                var rest = _uow.GetRepository<Restaurant>().LoadOne(id);
                return rest;
            }
            catch (Exception ex)
            {
                _log.LogError($"Error Loading Restaurant \n{ex.Message}");
                return null;
            }
        }

        public IEnumerable<Restaurant> ReadAllRestaurants()
        {
            try
            {
               var rest= _uow.GetRepository<Restaurant>().LoadAll();
               return rest;
            }catch(Exception ex)
            {
                _log.LogError($"Error Loading all Restaurants \n{ex.Message}");
                return null;
            }
        }

        public Restaurant UpdateOneRestaurant(Restaurant model, long id)
        {
            try
            {
                _uow.GetRepository<Restaurant>().Update(model);
                return model;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _log.LogError($"Error Updating {model.Name} \n{ex.Message}");
                return null;
            }
        }
    }

    public interface IRestaurantManager
    {
        Task<RestaurantModel> CreateRestaurant(RestaurantModel model);
        Restaurant LoadOneRestaurant(long id);
        Restaurant UpdateOneRestaurant(Restaurant model, long id);
        IEnumerable<Restaurant> ReadAllRestaurants();
    }
}
