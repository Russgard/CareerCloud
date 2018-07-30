using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository) { }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" - {poco.Id}"));
                else
                {
                    if (poco.CompanyWebsite.Length < 4)
                        exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" - {poco.Id}"));
                    else
                    {
                        if (poco.CompanyWebsite.Length == 4 && poco.CompanyWebsite.Substring(poco.CompanyWebsite.Length - 3) != ".ca")
                            exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" - {poco.Id}"));
                        else if (poco.CompanyWebsite.Length > 4 && poco.CompanyWebsite.Substring(poco.CompanyWebsite.Length - 3) != ".ca" && poco.CompanyWebsite.Substring(poco.CompanyWebsite.Length - 4) != ".com" && poco.CompanyWebsite.Substring(poco.CompanyWebsite.Length - 4) != ".biz")
                            exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" - {poco.Id}"));
                    }
                }
                
                if (string.IsNullOrEmpty(poco.ContactPhone))
                    exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                else
                {
                    string[] tokens = poco.ContactPhone.Split('-');
                    if (tokens.Length != 3)
                        exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                    else
                    {
                        if (int.TryParse(tokens[0], out int a))
                        {
                                if (a < 100 || a > 999)
                                    exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                                else
                                {
                                    if (int.TryParse(tokens[1], out int b))
                                    {
                                        if (b > 999)
                                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                                        else
                                        {
                                            if (int.TryParse(tokens[2], out int c))
                                            {
                                                if (c < 1000 || c > 9999)
                                                    exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                                            }
                                            else
                                                exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                                        }
                                    }
                                    else
                                        exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));

                                }
                        }
                        else
                            exceptions.Add(new ValidationException(601, $"Must correspond to a valid phone number (e.g. 416-555-1234) - {poco.Id}"));
                    }
                }
            }
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

        }
    }
}
