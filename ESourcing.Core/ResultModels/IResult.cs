namespace ESourcing.Core.ResultModels
{
    //TODO: Uzak sunucudaki cliente ulaşmak 5
    public interface IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
