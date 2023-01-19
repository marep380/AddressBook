using AddressBook_API.Models;

namespace AddressBook_API.Interfaces
{
    public interface IAddressBookService
    {
        /// <summary>
        /// Get all contacts.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> GetContacts();

        Task<int> GetContactsCount();

        /// <summary>
        /// Get contacts based on the page and contacts per page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="contactCount"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> GetContacts(int page, int contactCount);

        /// <summary>
        /// Search by first name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> SearchByFirstName(string firstName);

        /// <summary>
        /// Search by last name.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> SearchByLastName(string lastName);

        /// <summary>
        /// Search by address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> SearchByAddress(string address);

        /// <summary>
        /// Search by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<Contact>>> SearchByPhoneNumber(string phoneNumber);

        /// <summary>
        /// Create contact.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<ServiceResponse<Contact>> Create(Contact contact);

        /// <summary>
        /// Updates contact.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<ServiceResponse<Contact>> Update(Contact contact);

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Task<ServiceResponse<Contact>> Delete(int contactId);

    }
}
