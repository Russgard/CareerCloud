using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CareerCloud.WCF
{
    class Security : ISecurity
    {
        public void AddSecurityLogin(SecurityLoginPoco[] pocos)
        {
            SecurityLoginLogic logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(false));
            logic.Add(pocos);
        }

        public void AddSecurityLoginsLog(SecurityLoginsLogPoco[] pocos)
        {
            SecurityLoginsLogLogic logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(false));
            logic.Add(pocos);
        }

        public void AddSecurityLoginsRole(SecurityLoginsRolePoco[] pocos)
        {
            SecurityLoginsRoleLogic logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(false));
            logic.Add(pocos);
        }

        public void AddSecurityRole(SecurityRolePoco[] pocos)
        {
            SecurityRoleLogic logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(false));
            logic.Add(pocos);
        }

        public List<SecurityLoginPoco> GetAllSecurityLogin()
        {
            SecurityLoginLogic logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(false));
            return logic.GetAll();
        }

        public List<SecurityLoginsLogPoco> GetAllSecurityLoginsLog()
        {
            SecurityLoginsLogLogic logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(false));
            return logic.GetAll();

        }

        public List<SecurityLoginsRolePoco> GetAllSecurityLoginsRole()
        {
            SecurityLoginsRoleLogic logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(false));
            return logic.GetAll();
        }

        public List<SecurityRolePoco> GetAllSecurityRole()
        {
            SecurityRoleLogic logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(false));
            return logic.GetAll();
        }

        public SecurityLoginPoco GetSingleSecurityLogin(Guid Id)
        {
            SecurityLoginLogic logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(false));
            return logic.Get(Id);
        }

        public SecurityLoginsLogPoco GetSingleSecurityLoginsLog(Guid Id)
        {
            SecurityLoginsLogLogic logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(false));
            return logic.Get(Id);
        }

        public SecurityLoginsRolePoco GetSingleSecurityLoginsRole(Guid Id)
        {
            SecurityLoginsRoleLogic logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(false));
            return logic.Get(Id);
        }

        public SecurityRolePoco GetSingleSecurityRole(Guid Id)
        {
            SecurityRoleLogic logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(false));
            return logic.Get(Id);
        }

        public void RemoveSecurityLogin(SecurityLoginPoco[] pocos)
        {
            SecurityLoginLogic logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveSecurityLoginsLog(SecurityLoginsLogPoco[] pocos)
        {
            SecurityLoginsLogLogic logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveSecurityLoginsRole(SecurityLoginsRolePoco[] pocos)
        {
            SecurityLoginsRoleLogic logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveSecurityRole(SecurityRolePoco[] pocos)
        {
            SecurityRoleLogic logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(false));
            logic.Delete(pocos);
        }

        public void UpdateSecurityLogin(SecurityLoginPoco[] pocos)
        {
            SecurityLoginLogic logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>(false));
            logic.Update(pocos);
        }

        public void UpdateSecurityLoginsLog(SecurityLoginsLogPoco[] pocos)
        {
            SecurityLoginsLogLogic logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>(false));
            logic.Update(pocos);
        }

        public void UpdateSecurityLoginsRole(SecurityLoginsRolePoco[] pocos)
        {
            SecurityLoginsRoleLogic logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>(false));
            logic.Update(pocos);
        }

        public void UpdateSecurityRole(SecurityRolePoco[] pocos)
        {
            SecurityRoleLogic logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>(false));
            logic.Update(pocos);
        }
    }
}
