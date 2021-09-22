using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic
{
    public class CustomerLogic
    {

        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext _context;

        private EntityFrameworkCustomerRepository _customer;

        public CustomerLogic(DataBaseContext context)
        {
            _context = context;
            _customer = new EntityFrameworkCustomerRepository(_context);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customer.GetList();
        }



    }
}
