using AutoMapper;
using FourtitudeAsiaAssessment.Application.IService;
using FourtitudeAsiaAssessment.Domain;
using FourtitudeAsiaAssessment.DTO.Request;
using FourtitudeAsiaAssessment.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FourtitudeAsiaAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(TransactionController));

        public TransactionController(ITransactionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("/api/submittrxmessage")]
        public IActionResult SubmitTransaction([FromBody] TransactionRequestDTO request)
        {
            _logger.Info($"[REQUEST] SubmitTransaction: {JsonConvert.SerializeObject(request)}");

            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Where(x => x.Value.Errors.Any()).SelectMany(x => x.Value.Errors.Select(y => $"{FormatPropertyName(x.Key)}: {y.ErrorMessage}")).ToList();

                var errorResponse = new ErrorResponse
                {
                    Result = 0,
                    ResultMessage = "Validation Failed!",
                    Description = errorList
                };

                _logger.Warn($"[RESPONSE] SubmitTransaction (Validation Failed): {JsonConvert.SerializeObject(errorResponse)}");

                return BadRequest(errorResponse);
            }

            var transactionMapper = _mapper.Map<Transaction>(request);

            if (!_service.Authorization(transactionMapper))
            {
                var errorResponse = new ErrorResponse
                {
                    Result = 0,
                    ResultMessage = "Access Denied!",
                    Description = new List<string> { "Unauthorized partner or Signature Mismatch" }
                };

                _logger.Warn($"[RESPONSE] SubmitTransaction (Authorization Failed): {JsonConvert.SerializeObject(errorResponse)}");

                return BadRequest(errorResponse);
            }

            //if (!_service.ValidTotalAmount(transactionMapper))
            //{
            //    var errorResponse = new ErrorResponse
            //    {
            //        Result = 0,
            //        ResultMessage = "Invalid Total Amount.",
            //        Description = new List<string> { "The total value stated in itemDetails array not equal to value in totalamount." }
            //    };

            //    _logger.Warn($"[RESPONSE] SubmitTransaction (Invalid Total Amount): {JsonConvert.SerializeObject(errorResponse)}");

            //    return BadRequest(errorResponse);
            //}

            //if (_service.ExpiredRequest(transactionMapper.Timestamp))
            //{
            //    var errorResponse = new ErrorResponse
            //    {
            //        Result = 0,
            //        ResultMessage = "Expired.",
            //        Description = new List<string> { "API request is expired." }
            //    };

            //    _logger.Warn($"[RESPONSE] SubmitTransaction (Expired Request): {JsonConvert.SerializeObject(errorResponse)}");

            //    return BadRequest(errorResponse);
            //}

            var discountAmount = _service.CalculateDiscountAmount(transactionMapper.TotalAmount ?? 0);

            var successResponse = new SuccessResponse()
            {
                Result = 1,
                TotalAmount = request.TotalAmount,
                TotalDiscount = discountAmount,
                FinalAmount = request.TotalAmount - discountAmount
            };

            _logger.Info($"[RESPONSE] SubmitTransaction (Success): {JsonConvert.SerializeObject(successResponse)}");

            return Ok(successResponse);
        }

        private string FormatPropertyName(string key)
        {
            return char.ToLowerInvariant(key[0]) + key.Substring(1);
        }
    }
}
