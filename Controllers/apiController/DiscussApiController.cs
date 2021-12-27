using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity.Validation;
using LMSweb.Models;
using LMSweb.ViewModel;

namespace LMSweb.Controllers.ApiControllers
{
    public class DiscussApiController : ApiController
    {
        private LMSmodel db = new LMSmodel();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("live")]
        public IHttpActionResult AddDiscussApi(LearnBViewModel Discussapi)
        {

            try
            {
                if (ModelState.IsValid) //如果輸入驗證通過
                {
                    //將輸入的欄位一對一放進類別內
                    LearningBehavior vm = new LearningBehavior();

                    vm.ActionType = Discussapi.LearningBehavior.ActionType;
                    vm.subAction = Discussapi.LearningBehavior.subAction;
                    vm.Detail = Discussapi.LearningBehavior.Detail;
                    vm.Time = Discussapi.LearningBehavior.Time;
                    vm.StudentMissions.SID = Discussapi.LearningBehavior.StudentMissions.SID;
                    vm.StudentMissions.MID = Discussapi.LearningBehavior.StudentMissions.MID;

                    //加入到資料庫
                    db.LearnB.Add(vm);

                    //儲存
                    db.SaveChanges();

                    //回傳HTTP OK
                    return Ok(ModelState);
                }
                else //否則驗證失敗
                {
                    //回傳HTTP Bad Request
                    return BadRequest(ModelState);
                }
            }
            catch (DbUpdateException ex)
            {
                //回傳HTTP Internal Server Error
                return InternalServerError(ex);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}