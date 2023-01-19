using AddressBook_API.Models;

namespace AddressBook_API.Interfaces
{
    public interface IAddressBookRepository
    {
        Task<Contact?> GetContactByPhoneNumber(string phoneNumber);

        Task<List<Contact>> GetAllContacts();
        Task<List<Contact>> GetContactsByPhoneNumber(string phoneNumber);
        Task<List<Contact>> GetContactsByAddress(string address);
        Task<List<Contact>> GetContactsByFirstName(string firstName);
        Task<List<Contact>> GetContactsByLastName(string lastName);

        /// <summary>
        /// Retrieve contact based on the page number and the contacts per page count.
        /// </summary>
        /// <param name="page">Requested page</param>
        /// <param name="resultsPerPage">Contacts per page</param>
        /// <returns></returns>
        Task<List<Contact>> GetContacts(int page, int contactsPerPage);

        /// <summary>
        /// Get contact by id.
        /// </summary>
        /// <param name="contactId">Contact id.</param>
        /// <returns></returns>
        Task<Contact?> GetContactById(int contactId);

        /// <summary>
        /// Create new contact.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<Contact> CreateContact(Contact contact);

        /// <summary>
        /// Update contact.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<Contact> UpdateContact(Contact contact);

        /// <summary>
        /// Delete contact.
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Task<Contact> DeleteContact(int contactId);

    }
}
