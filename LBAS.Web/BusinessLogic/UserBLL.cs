using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LBAS.Web.Utils;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LBAS.Web.BusinessLogic
{
    public class UserBLL
    {
        public async Task<List<User>> GetUsers()
        {
            List<User> userList = new List<User>();
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IPagedCollection<IUser> pagedCollection = await client.Users.Where(user => user.UserType.Equals(Utils.Constants.RealUser)).ExecuteAsync();
                if (pagedCollection != null)
                {
                    do
                    {
                        List<IUser> users = pagedCollection.CurrentPage.ToList();
                        foreach (IUser user in users)
                        {
                            userList.Add((User)user);
                        }
                        pagedCollection = await pagedCollection.GetNextPageAsync();
                    } while (pagedCollection != null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userList;
        }

        public async Task<User> GetUser(string objectID)
        {
            User user = null;
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                user = (User)await client.Users.GetByObjectId(objectID).ExecuteAsync();
            }
            catch  (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public async Task<bool> CreateUser(User user)
        {
            ActiveDirectoryClient client = null;
            try
            {
                client = AuthenticationHelper.GetActiveDirectoryClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                await client.Users.AddUserAsync(user);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> EditUser(User user, FormCollection values)
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser toUpdate = await client.Users.GetByObjectId(user.ObjectId).ExecuteAsync();
                Helper.CopyUpdatedValues(toUpdate, user, values);
                await toUpdate.UpdateAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser toDelete = await client.Users.GetByObjectId(user.ObjectId).ExecuteAsync();
                await toDelete.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> GetDirectReports(string objectID)
        {
            List<User> reports = new List<User>();
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser user = await client.Users.GetByObjectId(objectID).ExecuteAsync();
                var userFetcher = user as IUserFetcher;
                IPagedCollection<IDirectoryObject> directReports = await userFetcher.DirectReports.ExecuteAsync();
                do
                {
                    List<IDirectoryObject> directoryObjects = directReports.CurrentPage.ToList();
                    foreach (IDirectoryObject directoryObject in directoryObjects)
                    {
                        if (directoryObject is User)
                        {
                            reports.Add((User) directoryObject);
                        }
                    }
                    directReports = await directReports.GetNextPageAsync();
                } while (directReports != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reports;
        }

        public async Task<User> GetManager(string objectID)
        {
            User manager = null;
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser user = await client.Users.GetByObjectId(objectID).ExecuteAsync();
                var userFetcher = user as IUserFetcher;
                manager = (User)await userFetcher.Manager.ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return manager;
        }

        public async Task<bool> AddManager(string objectID, User manager)
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser user = await client.Users.GetByObjectId(objectID).ExecuteAsync();
                user.Manager = manager;
                await user.UpdateAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}