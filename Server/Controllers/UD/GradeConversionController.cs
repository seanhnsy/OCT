﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    public class GradeConversionController : BaseController, GenericRestController<GradeConversionDTO>
    {
        public GradeConversionController(OCTOBEROracleContext context,
    IHttpContextAccessor httpContextAccessor,
    IMemoryCache memoryCache)
: base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{LetterGrade}")]
        public async Task<IActionResult> Delete(int SchoolID, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x=>x.SchoolId == SchoolID)
                    .Where(x => x.LetterGrade == LetterGrade)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeConversions.Remove(itm);
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

                var result = await _context.GradeConversions.Select(sp => new GradeConversionDTO
                {
                    SchoolId = sp.SchoolId,
                     LetterGrade = sp.LetterGrade,
                      CreatedBy = sp.CreatedBy,
                       CreatedDate = sp.CreatedDate,
                        GradePoint = sp.GradePoint,
                         MaxGrade = sp.MaxGrade,
                          MinGrade = sp.MinGrade,
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
        [Route("Get/{SchoolID}/{LetterGrade}")]
        public async Task<IActionResult> Get(int SchoolID, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeConversionDTO? result = await _context.GradeConversions
                    .Where(x=>x.SchoolId == SchoolID)
                    .Where(x => x.LetterGrade == LetterGrade)
                    .Select(sp => new GradeConversionDTO
                    {
                        SchoolId = sp.SchoolId,
                        LetterGrade = sp.LetterGrade,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        GradePoint = sp.GradePoint,
                        MaxGrade = sp.MaxGrade,
                        MinGrade = sp.MinGrade,
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
        public async Task<IActionResult> Post([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x=>x.SchoolId == _GradeConversionDTO.SchoolId)
                    .Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeConversion g = new GradeConversion
                    {
                        SchoolId = _GradeConversionDTO.SchoolId,
                        LetterGrade = _GradeConversionDTO.LetterGrade,
                        GradePoint = _GradeConversionDTO.GradePoint,
                        MaxGrade = _GradeConversionDTO.MaxGrade,
                        MinGrade = _GradeConversionDTO.MinGrade
                    };
                    _context.GradeConversions.Add(g);
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
        public async Task<IActionResult> Put([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x=>x.SchoolId == _GradeConversionDTO.SchoolId)
                    .Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade).FirstOrDefaultAsync();

                itm.SchoolId = _GradeConversionDTO.SchoolId;
                itm.LetterGrade = _GradeConversionDTO.LetterGrade;
                itm.GradePoint = _GradeConversionDTO.GradePoint;
                itm.MaxGrade = _GradeConversionDTO.MaxGrade;
                itm.MinGrade = _GradeConversionDTO.MinGrade;

                _context.GradeConversions.Update(itm);
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
