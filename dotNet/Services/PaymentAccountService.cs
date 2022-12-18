using Sabio.Data.Providers;
using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Data;
using Sabio.Services.Interfaces;
using Sabio.Models;
using System.Collections;
using Sabio.Models.Requests.PaymentAccount;

namespace Sabio.Services
{
    public class PaymentAccountService : IPaymentAccountService
    {
        IDataProvider _data = null;
        public PaymentAccountService(IDataProvider data)
        {
            _data = data;
        }

        public PaymentAccount SelectById(int id)
        {
            string procName = "[dbo].[PaymentAccounts_Select_ById]";
            PaymentAccount account = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName,
            inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", id);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                account = MapSingleAccount(reader, ref totalCount);
            });
            return account;
        }

        public int Add(PaymentAccountAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[PaymentAccounts_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, userId, col);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                });
            return id;
        }

        public void Update(PaymentAccountUpdateRequest model, int userId)
        {
            string procName = "[dbo].[PaymentAccounts_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, userId, col);
                    col.AddWithValue("@Id", model.Id);
                },
                returnParameters: null);
        }

        public void Delete(int id)
        {
            string procName = "[dbo].[PaymentAccounts_Delete_ById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                },
                returnParameters: null);
        }


        public Paged<PaymentAccount> SelectAllPagination(int pageIndex, int pageSize)
        {
            Paged<PaymentAccount> pagedList = null;
            List<PaymentAccount> list = null;
            int totalCount = 0;
            string procName = "[dbo].[PaymentAccounts_SelectAll_Paginated]";
            _data.ExecuteCmd(procName
                , inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);

                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    PaymentAccount account = MapSingleAccount(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (list == null)
                    {
                        list = new List<PaymentAccount>();
                    }
                    list.Add(account);
                });
            if (list != null)
            {
                pagedList = new Paged<PaymentAccount>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<PaymentAccount> SelectByCreatedByPagination(int pageIndex, int pageSize, int id)
        {
            Paged<PaymentAccount> pagedList = null;
            List<PaymentAccount> list = null;
            int totalCount = 0;
            string procName = "[dbo].[PaymentAccounts_Select_ByCreatedBy_Paginated]";
            _data.ExecuteCmd(procName
                , inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    parameterCollection.AddWithValue("@CreatedBy", id);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    PaymentAccount friend = MapSingleAccount(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (list == null)
                    {
                        list = new List<PaymentAccount>();
                    }
                    list.Add(friend);
                });
            if (list != null)
            {
                pagedList = new Paged<PaymentAccount>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        private static PaymentAccount MapSingleAccount(IDataReader reader, ref int startingIndex)
        {
            PaymentAccount account = new PaymentAccount();
            account.Id = reader.GetSafeInt32(startingIndex++);
            account.AccountId = reader.GetSafeString(startingIndex++);
            account.PaymentTypeId = reader.GetSafeInt32(startingIndex++);
            account.DateCreated = reader.GetSafeDateTime(startingIndex++);
            account.DateModified = reader.GetSafeDateTime(startingIndex++);
            account.CreatedBy = reader.GetSafeInt32(startingIndex++);
            account.ModifiedBy = reader.GetSafeInt32(startingIndex++);
            return account;
        }

        private static void AddCommonParams(PaymentAccountAddRequest model, int userId, SqlParameterCollection col)
        {
            col.AddWithValue("@AccountId", model.AccountId);
            col.AddWithValue("@PaymentTypeId", model.PaymentTypeId);
            col.AddWithValue("@UserId", userId);
        }
    }
}
