namespace FourtitudeAsiaAssessment.DTO.Response
{
    public class ErrorResponse
    {
        public int Result { get; set; } = 0;

        public string ResultMessage { get; set; }

        public List<string> Description { get; set; }
    }
}
