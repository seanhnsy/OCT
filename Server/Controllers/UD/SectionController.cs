using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
    IHttpContextAccessor httpContextAccessor,
    IMemoryCache memoryCache)
: base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SectionID}/{SchoolID}")]
        public async Task<IActionResult> Delete(int SectionID, int SchoolID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                     SchoolId = sp.SchoolId,
                      SectionId = sp.SectionId,
                       Capacity = sp.Capacity,
                        CourseNo = sp.CourseNo,
                         InstructorId = sp.InstructorId,
                          Location = sp.Location,
                           SectionNo = sp.SectionNo,
                            StartDateTime = sp.StartDateTime,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{SectionID}/{SchoolID}")]
        public async Task<IActionResult> Get(int SectionID, int SchoolID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context.Sections
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Select(sp => new SectionDTO
                    {
                        SchoolId = sp.SchoolId,
                        SectionId = sp.SectionId,
                        Capacity = sp.Capacity,
                        CourseNo = sp.CourseNo,
                        InstructorId = sp.InstructorId,
                        Location = sp.Location,
                        SectionNo = sp.SectionNo,
                        StartDateTime = sp.StartDateTime,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate
                    })
                .SingleAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections
                    .Where(x => x.SectionId == _SectionDTO.SectionId)
                    .Where(x => x.SchoolId == _SectionDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Section s = new EF.Models.Section
                    {
                        SchoolId = _SectionDTO.SchoolId,
                        SectionId = _SectionDTO.SectionId,
                        Capacity = _SectionDTO.Capacity,
                        CourseNo = _SectionDTO.CourseNo,
                        InstructorId = _SectionDTO.InstructorId,
                        Location = _SectionDTO.Location,
                        SectionNo = _SectionDTO.SectionNo,
                        StartDateTime = _SectionDTO.StartDateTime,
                    };
                    _context.Sections.Add(s);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections
                    .Where(x => x.SectionId == _SectionDTO.SectionId)
                    .Where(x=>x.SchoolId == _SectionDTO.SchoolId)
                    .FirstOrDefaultAsync();

                itm.SchoolId = _SectionDTO.SchoolId;
                itm.SectionId = _SectionDTO.SectionId;
                itm.Capacity = _SectionDTO.Capacity;
                itm.CourseNo = _SectionDTO.CourseNo;
                itm.InstructorId = _SectionDTO.InstructorId;
                itm.Location = _SectionDTO.Location;
                itm.SectionNo = _SectionDTO.SectionNo;
                itm.StartDateTime = _SectionDTO.StartDateTime;

                _context.Sections.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}
