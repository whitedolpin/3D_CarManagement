/*
 * Data Source=(localdb)\mssqllocaldb;Initial Catalog=PRN_3D;Integrated Security=True
 
using MaiDTS.Asm2.Repo.Repository;
using Pizzaa.Data;
using Pizzaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UnitOfWork 
    {
        private PrnAsm2Context context = new PrnAsm2Context();

        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<Customer> cutomerRepository;
        private GenericRepository<OrderDetail> orderdetailRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<Supplier> supplierRepository;
        private GenericRepository<Account> accountRepository;

        public GenericRepository<Category> CategoryRepository
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }
        public GenericRepository<Customer> CutomerRepository
        {
            get
            {

                if (this.cutomerRepository == null)
                {
                    this.cutomerRepository = new GenericRepository<Customer>(context);
                }
                return cutomerRepository;
            }
        }
        public GenericRepository<OrderDetail> OrderdetailRepository
        {
            get
            {

                if (this.orderdetailRepository == null)
                {
                    this.orderdetailRepository = new GenericRepository<OrderDetail>(context);
                }
                return orderdetailRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }
        public GenericRepository<Supplier> SupplierRepository
        {
            get
            {

                if (this.supplierRepository == null)
                {
                    this.supplierRepository = new GenericRepository<Supplier>(context);
                }
                return supplierRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }
        public GenericRepository<Account> AccountRepository
        {
            get
            {

                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(context);
                }
                return accountRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

 
 */