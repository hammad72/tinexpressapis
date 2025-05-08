using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public readonly DbResource Options; private readonly IConfiguration _configuration;

        public OrderDetailsRepository(OrderDbContext orderDbContext, IOptions<DbResource> options, IConfiguration configuration)
        {
            _orderDbContext = orderDbContext;
            Options = options.Value; _configuration = configuration;

        }

        public async Task<string> AddAsync(orderdetails o, List<orderitems> oid)
        {
            string ConsignmentNumber = "";
            using var transaction = await _orderDbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                List<orderitems> oiList = new List<orderitems>();
                ConsignmentNumber = generateCNUM();
                o.consignment_number = ConsignmentNumber;
                o.order_status_id = 1;
                o.order_status_change_date = dt;
                await _orderDbContext.orderdetails.AddAsync(o);
                await _orderDbContext.SaveChangesAsync();

                foreach (var item in oid)
                {
                    orderitems oi = new orderitems();
                    oi.consignment_number = ConsignmentNumber;
                    oi.order_number = item.order_number;
                    oi.package_type_id = item.package_type_id;
                    oi.package_type = item.package_type;
                    oi.package_content_id = item.package_content_id;
                    oi.package_content = item.package_content;
                    oi.weight = item.weight;
                    oi.actual_weight = item.actual_weight;
                    oi.rider_weight = item.rider_weight;
                    oi.width = item.width;
                    oi.length = item.length;
                    oi.height = item.height;
                    oiList.Add(oi);
                }

                await _orderDbContext.orderitems.AddRangeAsync(oiList);
                await _orderDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception
            }

            return ConsignmentNumber;


            //using (var transaction = _orderDbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted))
            //{
            //    orderdetails od = new orderdetails();
            //    List<orderitems> oiList = new List<orderitems>();
            //    ConsignmentNumber = generateCNUM();
            //    od.consignment_number = ConsignmentNumber;
            //    await _orderDbContext.orderdetails.AddAsync(od);
            //    foreach (var item in o.oid)
            //    {
            //        orderitems oi = new orderitems();
            //        oi.consignment_number = ConsignmentNumber;
            //        oi.order_number = item.order_number;
            //        oi.package_type_id = item.package_type_id;
            //        oi.package_type = item.package_type;
            //        oi.package_content_id = item.package_content_id;
            //        oi.package_content = item.package_content;
            //        oi.weight = item.weight;
            //        oi.actual_weight = item.actual_weight;
            //        oi.rider_weight = item.rider_weight;
            //        oi.width = item.width;
            //        oi.length = item.length;
            //        oi.height = item.height;
            //        oiList.Add(oi);
            //    }
            //    await _orderDbContext.orderitems.AddRangeAsync(oiList);
            //    await _orderDbContext.SaveChangesAsync();
            //    //pid = p.id;
            //    transaction.
            //}
            //return ConsignmentNumber;
        }

        public Task<List<order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<order> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        private string generateCNUM() // Stored Procedure
        {
            try
            {
                string cnum = "";
                //string connectionString = Options.ConnectionString;
                string connectionString = _configuration.GetConnectionString("DevConnectionOrder");

                MySqlTransaction objTrans = null;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("GetNewCN_Func", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var returnParameter = cmd.Parameters.Add("@ReturnVal", (DbType)SqlDbType.VarChar);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        con.Open();
                        objTrans = con.BeginTransaction(System.Data.IsolationLevel.Serializable);
                        cmd.Transaction = objTrans;
                        cmd.ExecuteNonQuery();
                        var result = returnParameter.Value;
                        if (result != null)
                            objTrans.Commit();
                        else
                            objTrans.Rollback();
                        try { cnum = result.ToString(); }
                        catch { cnum = ""; }
                    }
                }
                return cnum;
            }
            catch (Exception ex)
            {
                return null; //throw ex;
            }
        }
        public async Task<orderdetails> getOrderByConsignmentAsync(string consignment)
        {
            try
            {

                var query = _orderDbContext.orderdetails.AsQueryable();

                var order = await query
                    .Where(x => x.consignment_number == consignment)
                    .FirstOrDefaultAsync();

                return order;
            }
            catch (Exception)
            {

                throw;
            } 

            }
    }
}
