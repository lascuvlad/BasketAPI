namespace BasketAPI.Helpers
{
    public class Errors
    {
        public string? Error { get; set; }
        public string? ErrorDescription { get; set; }
    }

    public class Succes
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    public static class SuccessMessage
    {
        public static Succes AddSuccess
            => new()
            {
                Success = true,
                Message = "Operation done with success. Your entity was added into the database."
            };

        public static Succes UpdateSuccess
            => new()
            {
                Success = true,
                Message = "Operation done with success. Your entity was updated successfully."
            };
    }

    public static class EndpointErrors
    {
        public static Errors InternalServerError
            => new()
            {
                Error = "internalServerError",
                ErrorDescription = "Internal server error."
            };

        public static Errors BasketInvalidStatus
            => new()
            {
                Error = "BasketInvalidStatus",
                ErrorDescription = "Please insert a valid status for the basket."
            };

        public static Errors FailedToBasketDetails
            => new()
            {
                Error = "failedToBasketDetails",
                ErrorDescription = "Failed to retreive basket informations."
            };

        public static Errors CustomerIsMandatory
            => new()
            {
                Error = "customerIsMandatory",
                ErrorDescription = "Customer is mandatory."
            };

        public static Errors ArticleNameIsMandatory
            => new()
            {
                Error = "articleNameIsMandatory",
                ErrorDescription = "Please specify the article name."
            };

        public static Errors StatusIsMandatory
            => new()
            {
                Error = "statusIsMandatory",
                ErrorDescription = "Please specify the status."
            };

        public static Errors SameStatus(string status)
            => new()
            {
                Error = "sameStatus",
                ErrorDescription = $"Status is already '{status}'."
            };

        public static Errors ArticlePriceIsMandatory
            => new()
            {
                Error = "articlePriceIsMandatory",
                ErrorDescription = "Please specify the article price."
            };

        public static Errors BasketNotFound
            => new()
            {
                Error = "basketNotFound",
                ErrorDescription = "Basket not found."
            };

        public static Errors BaskedClosed
            => new()
            {
                Error = "baskedClosed",
                ErrorDescription = "Basket is closed."
            };

        public static Errors AddBasketFailed
            => new()
            {
                Error = "addBasketFailed",
                ErrorDescription = "Add basket failed."
            };

        public static Errors AddBasketArticleFailed
            => new()
            {
                Error = "addBasketArticleFailed",
                ErrorDescription = "Add basket article failed."
            };

        public static Errors UpdateBasketStatusFailed
            => new()
            {
                Error = "updateBasketStatusFailed",
                ErrorDescription = "Update basket status failed."
            };
    }
}
