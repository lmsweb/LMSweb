using LMSweb.Models;
using LMSweb.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSweb.Controllers
{
    [RoutePrefix("api/test")]
    public class DiscussApiController : ApiController
    {
        private LMSmodel db = new LMSmodel();

        [HttpPost]
        [Route("live")]
        public IHttpActionResult AddDiscussApi([FromBody]DiscussViewModel Discussapi)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    LearningBehavior lb = new LearningBehavior();
                    //StudentMission data = db.StudentMissions
                    //.Where(x => x.MID == Discussapi.MID && x.SID == Discussapi.SID)
                    //.FirstOrDefault();
                    
                    lb.ActionType = Discussapi.ActionType;
                    lb.subAction = Discussapi.subAction;
                    lb.Detail = Discussapi.Detail;
                    lb.Time = Discussapi.Time;
                    lb.CID = Discussapi.CID;
                    //lb.StudentMissions = data;
                    lb.student.SID = Discussapi.SID;
                    lb.mission.MID = Discussapi.MID;

                    db.LearnB.Add(lb);
                    db.SaveChanges();
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