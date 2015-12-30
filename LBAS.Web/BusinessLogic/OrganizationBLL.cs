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
    public class OrganizationBLL
    {
        [Authorize(Roles = "LAdmin")]
        public async Task<List<User>> GetCorporations()
        {
            List<User> userList = new List<User>();
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IPagedCollection<IUser> pagedCollection = await client.Users.Where(user => user.UserType.Equals(Utils.Constants.VirtualUser) && user.Department.Equals(Utils.Constants.Corporation)).ExecuteAsync();
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

        public async Task<List<User>> GetFranchises()
        {
            List<User> userList = new List<User>();
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IPagedCollection<IUser> pagedCollection = await client.Users.Where(user => user.UserType.Equals(Utils.Constants.VirtualUser) && user.Department.Equals(Utils.Constants.Franchise)).ExecuteAsync();
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

        public async Task<List<User>> GetSites()
        {
            List<User> userList = new List<User>();
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IPagedCollection<IUser> pagedCollection = await client.Users.Where(user => user.UserType.Equals(Utils.Constants.VirtualUser) && user.Department.Equals(Utils.Constants.Site)).ExecuteAsync();
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

        public async Task<List<User>> GetFranchisesOfCorporation(string objectID)
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
                        if (!(directoryObject is User)) continue;

                        User tmp = (User)directoryObject;
                        if (tmp.UserType.Equals(Utils.Constants.VirtualUser))
                        {
                            reports.Add((User)directoryObject);
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

        public async Task<List<User>> GetSitesOfFranchise(string objectID)
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
                        if (!(directoryObject is User)) continue;

                        User tmp = (User)directoryObject;
                        if (tmp.UserType.Equals(Utils.Constants.VirtualUser))
                        {
                            reports.Add((User)directoryObject);
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

        public async Task<bool> CreateCorporation(User corp)
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
                await client.Users.AddUserAsync(corp);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CreateFranchise(User franchise, User corp)
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
                franchise.Manager = corp;
                await client.Users.AddUserAsync(franchise);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CreateSite(User site, User franchise)
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
                franchise.Manager = franchise;
                await client.Users.AddUserAsync(site);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}