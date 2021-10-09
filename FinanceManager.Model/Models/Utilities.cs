//////using Microsoft.AspNetCore.Http;
//////using Microsoft.AspNetCore.Mvc;
//////using Microsoft.AspNetCore.Mvc.Filters;
//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;

//////namespace FinanceManager.Model.Models
//////{
//////    class Utilities
//////    {
//////    }
//////    public class GiftAidAuthorize : Attribute, IAuthorizationFilter
//////    {


//////        public void OnAuthorization(AuthorizationFilterContext context)
//////        {
//////            try
//////            {
//////                var memberId = GetValue(context, "MemberId");
//////                if (string.IsNullOrWhiteSpace(memberId)) throw new Exception("MemberId is invalid");
//////                var telephone = GetValue(context, "Telephone");
//////                if (string.IsNullOrWhiteSpace(memberId)) throw new Exception("Telephone is invalid");
//////                context.Result = null;
//////            }
//////            catch (Exception ex)
//////            {

//////                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
//////                //context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes($"{ex.Message}"), 0, 250);
//////                context.Result = new JsonResult(new { message = $"{ex.Message}", statusCode = StatusCodes.Status403Forbidden });

//////            }
//////        }

//////        private static string GetValue(AuthorizationFilterContext context, string headerKey)
//////        {
//////            //check credentials before allowing access to resource
//////            return context.HttpContext.Request.Headers.Where(x => x.Key.ToLower() == headerKey.ToLower()).Select(x => x.Value).FirstOrDefault();
//////        }
//////    }


//////    public static class DataConstantQuery
//////    {
//////        public static Dictionary<GetQueryEnum, string> GetQuery
//////        {
//////            get
//////            {
//////                //giftaid queries
//////                Dictionary<GetQueryEnum, string> keyValuePairs = new Dictionary<GetQueryEnum, string>();

//////                keyValuePairs.Add(GetQueryEnum.GiftAidMemberBranchInfo, "SELECT * FROM Branches.Branch where Id=@Id");
//////                keyValuePairs.Add(GetQueryEnum.GiftAidMemberInfo, "SELECT FirstName,LastName FROM Members.Member where MemberId=@Id AND BranchID=@branchId ");
//////                keyValuePairs.Add(GetQueryEnum.GiftAidMemberGiftAidInfo, "SELECT * FROM Members.GiftAid where MemberId=@Id");
//////                keyValuePairs.Add(GetQueryEnum.GiftAidMemberGivingInfo, "SELECT * FROM Members.Giving where MemberId=@Id Order by Id desc");
//////                keyValuePairs.Add(GetQueryEnum.GiftAidMemberPermission, "SELECT m.MemberID,m.BranchID,CONCAT(m.FirstName,' ',m.LastName) GiftAidCordinator,m.Telephone, p.PagePermission FROM Members.LoginPermissionSet p inner join members.Member m  on p.MemberID = m.MemberID where p.PagePermission IN('Gift Aid','SuperUser') and m.BranchID=@BranchId");
//////                keyValuePairs.Add(GetQueryEnum.GiftAidGetBranchGivings, "SELECT * FROM Members.Giving  Order by Id desc");
//////                //permissions
//////                keyValuePairs.Add(GetQueryEnum.Permission, "select * from Members.LoginPermissionSet ");
//////                keyValuePairs.Add(GetQueryEnum.PermissionFilterByFilterName, "select m.MemberID,m.BranchID,b.Name as BranchName,m.LastName,m.FirstName,m.Telephone,p.ID as PermissionId,p.PagePermission from Members.LoginPermissionSet p inner join Members.Member m on m.MemberID=p.MemberID inner join Branches.Branch b on b.ID=m.BranchID where p.PagePermission=@permissionName");
//////                keyValuePairs.Add(GetQueryEnum.PermissionFilterByMemberName, "Select MemberId From Members.LoginPermissionset m WHERE m.MemberId=@MemberID And m.PagePermission=@PagePermission");

//////                //member get queries
//////                keyValuePairs.Add(GetQueryEnum.MemberBranchMembers, "SELECT * FROM Members.Member  where BranchId=@branchId Order by LastName");
//////                keyValuePairs.Add(GetQueryEnum.MemberCredentials, "SELECT MemberId,branchid,FirstName,LastName,Telephone FROM Members.Member WHERE MemberId=@memberId AND Telephone='@telephone'");
//////                keyValuePairs.Add(GetQueryEnum.MemberInfo, "SELECT MemberId,AreaID,BranchID,DistrictID,FirstName,LastName FROM Members.Member where MemberId=@Id");
//////                //branch get queries
//////                keyValuePairs.Add(GetQueryEnum.BranchMembers, "SELECT m.MemberID,m.BranchID,m.FirstName,m.LastName FROM Members.Member m where BranchId=@Id order by LastName asc, FirstName asc");

//////                //district get queries
//////                keyValuePairs.Add(GetQueryEnum.GetDistrict, "SELECT * FROM Churches.District where Id=@Id");

//////                //branches
//////                keyValuePairs.Add(GetQueryEnum.Branches, "select Id,Name,AreaID,DistrictID from Branches.Branch Order by Name");

//////                return keyValuePairs;
//////            }
//////        }
//////        public static Dictionary<PostQueryEnum, string> PostQuery
//////        {
//////            get
//////            {
//////                //giftaid queries
//////                Dictionary<PostQueryEnum, string> keyValuePairs = new Dictionary<PostQueryEnum, string>();
//////                keyValuePairs.Add(PostQueryEnum.PostGiftAid, "INSERT INTO Members.GIFTAID(Memberid,BranchID,NINumber,GIftAidId,DateRegistered,CreatedOn,CreatedBy) VALUES(@Memberid,@BranchID,@NINumber,@GIftAidId,@DateRegistered,@CreatedOn,@CreatedBy)");

//////                keyValuePairs.Add(PostQueryEnum.PostGiving, "INSERT INTO Members.Giving(Memberid,BranchID,Date,Fund,Method,Amount,GiftAid,Status,EnvelopeCount,CreatedBy,CreatedOn) VALUES(@Memberid,@BranchID,@Date,@Fund,@Method,@Amount,@GiftAid,@Status,@EnvelopeCount,@CreatedBy,@CreatedOn)");

//////                keyValuePairs.Add(PostQueryEnum.PostPermission, "Insert into Members.LoginPermissionSet(BranchID,MemberID,PagePermission,DateCreated,CreateBy)values(@BranchID,@MemberID,@PagePermission,@DateCreated,@CreateBy)");

//////                return keyValuePairs;
//////            }
//////        }
//////        public static Dictionary<DeleteQueryEnum, string> DeleteQuery
//////        {
//////            get
//////            {
//////                //giftaid queries
//////                Dictionary<DeleteQueryEnum, string> keyValuePairs = new Dictionary<DeleteQueryEnum, string>();
//////                keyValuePairs.Add(DeleteQueryEnum.DeleteGiving, "DELETE FROM Members.Giving where ID=@Id");
//////                //Permission queries
//////                keyValuePairs.Add(DeleteQueryEnum.DeletePermission, "DELETE FROM Members.LoginPermissionSet WHERE ID=@Id");
//////                return keyValuePairs;
//////            }
//////        }
//////        public static Dictionary<UpdateQueryEnum, string> UpdateQuery
//////        {
//////            get
//////            {
//////                //giftaid queries
//////                Dictionary<UpdateQueryEnum, string> keyValuePairs = new Dictionary<UpdateQueryEnum, string>();
//////                keyValuePairs.Add(UpdateQueryEnum.UpdateGiving, "UPDATE Members.Giving SET Memberid=@Memberid,BranchID=@BranchID,Date=@Date,Fund=@Fund,Method=@Method,Amount=@Amount,GiftAid=@GiftAid,Status=@Status,EnvelopeCount=@EnvelopeCount,CreatedBy=@CreatedBy,CreatedOn=@CreatedOn Where Id=@Id ");
//////                return keyValuePairs;
//////            }
//////        }
//////    }
//////    public enum GetQueryEnum
//////    {
//////        //gift aid enum
//////        GiftAidMemberBranchInfo,
//////        GiftAidMemberInfo,
//////        GiftAidMemberGiftAidInfo,
//////        GiftAidMemberGivingInfo,
//////        GiftAidMemberPermission,
//////        GiftAidGetBranchGivings,
//////        //branch enum
//////        BranchMembers,
//////        //member enum
//////        MemberInfo,
//////        MemberCredentials,
//////        MemberBranchMembers,

//////        //District enums
//////        GetDistrict,

//////        //permission
//////        PermissionFilterByFilterName,
//////        PermissionFilterByMemberName,
//////        //branches
//////        Branches,
//////        Permission,

//////    }
//////    public enum DeleteQueryEnum
//////    {
//////        //Giving
//////        DeleteGiving,
//////        //permission
//////        DeletePermission
//////    }
//////    public enum PostQueryEnum
//////    {
//////        //gift aid post
//////        PostGiftAid,
//////        PostGiving,
//////        PostPermission,
//////    }
//////    public enum UpdateQueryEnum
//////    {
//////        UpdateGiving,

//////    }

//////    public interface ICRUD<T>
//////    {
//////        Task<List<T>> Get(int branchId);
//////        Task<List<T>> Get();
//////        Task<List<T>> GetAll();
//////    }

//////    public class BranchRepository : ServiceClient, IBranchRepository
//////    {
//////        private readonly AuthenticatedUserService authenticatedUserService;
//////        public Dictionary<DeleteQueryEnum, string> DeleteQuery { get; }
//////        public Dictionary<GetQueryEnum, string> GetQuery { get; }
//////        public Dictionary<PostQueryEnum, string> PostQuery { get; }
//////        public Dictionary<UpdateQueryEnum, string> UpdateQuery { get; }


//////        public BranchRepository(IConfiguration configuration, AuthenticatedUserService authenticatedUserService) : base(configuration)
//////        {
//////            this.authenticatedUserService = authenticatedUserService;

//////            GetQuery = DataConstantQuery.GetQuery;
//////            DeleteQuery = DataConstantQuery.DeleteQuery;
//////            PostQuery = DataConstantQuery.PostQuery;
//////            UpdateQuery = DataConstantQuery.UpdateQuery;
//////        }
//////        public async Task<List<BranchObject>> GetAllBranches()
//////        {
//////            var branches = ExecuteQuery<BranchObject>(GetQuery[GetQueryEnum.Branches]);
//////            return branches;
//////        }
//////    }

//////    internal async Task<JsonResult> Validate(int MemberId, string Telephone)
//////    {
//////        if (MemberId <= 0 || string.IsNullOrWhiteSpace(Telephone))
//////            throw new Exception("Validation failed");

//////        var memberInfo = serviceClient.ExecuteQuery<Member>(
//////            $"SELECT MemberId,branchid,FirstName,LastName,Telephone FROM Members.Member WHERE MemberId={MemberId} AND Telephone='{Telephone}'"
//////            ).FirstOrDefault();

//////        //validation successfull invoke next middleware
//////        if (memberInfo == null) throw new Exception("Invalid MemberId or Telephone");

//////        return new JsonResult(new
//////        {
//////            Data = new List<BranchObject>() { new BranchObject() { Id = memberInfo.BranchID } },
//////            Error = "success"
//////        });
//////    }
//////}
