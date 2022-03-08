using Business.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AgreementManager : IAgreementManager
    {
        private readonly IAgreementDal agreementDal;

        public AgreementManager(IAgreementDal agreementDal)
        {
            this.agreementDal = agreementDal;
        }


    }
}
