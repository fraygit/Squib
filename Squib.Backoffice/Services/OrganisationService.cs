using Squib.Data.Interface;
using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Squib.Backoffice.Services
{
    public class OrganisationService
    {
        private IOrganisationRepository _organisationRepository;
        private IUserRepository _userRepository;

        public OrganisationService(IOrganisationRepository _organisationRepository, IUserRepository _userRepository)
        {
            this._organisationRepository = _organisationRepository;
            this._userRepository = _userRepository;
        }

        public async Task<List<Organisation>> Get(string email)
        {
            var userOrganisations = new List<Organisation>();
            var user = await _userRepository.GetUser(email);
            if (user != null)
            {
                if (user.Organisations != null)
                {
                    foreach (var organisation in user.Organisations)
                    {
                        var org = await _organisationRepository.Get(organisation.ToString());
                        if (org != null)
                        {
                            userOrganisations.Add(org);
                        }
                    }
                }
            }
            return userOrganisations;
        }
    }
}