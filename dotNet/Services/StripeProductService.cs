using Amazon.Runtime.Internal;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class StripeProductService : IStripeProductService
    {
        IDataProvider _data = null;
        public StripeProductService(IDataProvider data)
        {
            _data = data;
        }

        public StripeProduct GetById(int id)
        {
            string procName = "[dbo].[StripeProducts_SelectById]";
            StripeProduct product = null;
            _data.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    product = StripeProductMapper(reader);
                });
            return product;
        }

        private static StripeProduct StripeProductMapper(IDataReader reader)
        {
            StripeProduct product = new StripeProduct();
            int startingIndex = 0;
            product.Id = reader.GetSafeInt32(startingIndex++);
            product.Name = reader.GetString(startingIndex++);
            product.ProductId = reader.GetString(startingIndex++);
            product.PriceId = reader.GetString(startingIndex++);
            product.Amount = reader.GetSafeDecimal(startingIndex++);
            product.Term = reader.GetSafeString(startingIndex++);
            return product;
        }
    }
}
