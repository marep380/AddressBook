namespace AddressBook_API.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool Successfull { get; set; } = true;

        public string? Message { get; set; }

    }
}
