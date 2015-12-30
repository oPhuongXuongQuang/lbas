using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.Owin.Security.OpenIdConnect;
using LBAS.Web.Utils;

namespace LBAS.Web.BusinessLogic
{
    public class RoleBLL
    {
        public async Task<List<AppRole>> GetRoleList()
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IPagedCollection<IApplication> collecitons = (IPagedCollection<IApplication>)await client.Applications.ExecuteAsync();
                List<IApplication> list = collecitons.CurrentPage.ToList();
                foreach (var app in list)
                {
                    if (String.Equals(app.AppId, LBAS.Web.Utils.Constants.ClientId))
                    {
                        IList<AppRole> roles = app.AppRoles;
                        return new List<AppRole>(roles);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public async Task<bool> AssignRole(User user, AppRole role)
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                AppRoleAssignment appRoleAssignment = new AppRoleAssignment();
                /*
                 * id = approle id
                 * principle id = user objectid
                 * resource id = service principle objectid
                 */
                appRoleAssignment.Id = role.Id;
                appRoleAssignment.PrincipalId = Guid.Parse(user.ObjectId);
                appRoleAssignment.ResourceId = Guid.Parse(LBAS.Web.Utils.Constants.ClientId);
                IList<AppRoleAssignment> appRoleAssignments = new List<AppRoleAssignment>() { appRoleAssignment };
                user.AppRoleAssignments = appRoleAssignments;
                await user.UpdateAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}