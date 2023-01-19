using AddressBook_API.Interfaces;
using AddressBook_API.Models;

namespace AddressBook_API.Services
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository bookRepository;

        public AddressBookService(IAddressBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<ServiceResponse<Contact>> Create(Contact contact)
        {
            ServiceResponse<Contact> serviceResponse = new ServiceResponse<Contact>();

            // Check for all fields
            if (!AllFieldsCheck(contact))
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "All fields required!";
                return serviceResponse;
            }

            // Check if a phone number allready exist
            var contactDb = await this.bookRepository.GetContactByPhoneNumber(contact.PhoneNumber);

            if (contactDb != null)
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Phone number allready exist!";
                return serviceResponse;
            }

            await this.bookRepository.CreateContact(contact);

            serviceResponse.Data = contact;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Contact>> Delete(int contactId)
        {
            ServiceResponse<Contact> serviceResponse = new ServiceResponse<Contact>();

            // Check if a phone number exist
            var contactDb = await this.bookRepository.GetContactById(contactId);

            if (contactDb == null)
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Contact not found!";
                return serviceResponse;
            }

            var deletedContact = await this.bookRepository.DeleteContact(contactDb.Id);
            if (deletedContact != null)
            {
                serviceResponse.Data = deletedContact;
                serviceResponse.Message = "Contact deleted successfully.";
                return serviceResponse;
            }

            serviceResponse.Successfull = false;
            serviceResponse.Message = "Something went wrong!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Contact>>> GetContacts()
        {
            var contacts = await this.bookRepository.GetAllContacts();
            return new ServiceResponse<List<Contact>> { Data = contacts };
        }
        public async Task<int> GetContactsCount()
        {
            var contacts = await this.bookRepository.GetAllContacts();
            return contacts.Count;
        }

        public async Task<ServiceResponse<List<Contact>>> GetContacts(int page, int contactsPerPage)
        {
            var pageResult = 1f;
            var contacts = await this.bookRepository.GetContacts(page, contactsPerPage);

            var searchedContacts = await this.bookRepository.GetAllContacts();
            return new ServiceResponse<List<Contact>> { Data = contacts };
        }

        public async Task<ServiceResponse<List<Contact>>> SearchByAddress(string address)
        {
            ServiceResponse<List<Contact>> serviceResponse = new ServiceResponse<List<Contact>>();

            if (!IsValidSearch(address))
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Invalid address!";
                return serviceResponse;
            }

            var contacts = await this.bookRepository.GetContactsByAddress(address);
            serviceResponse.Data = contacts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Contact>>> SearchByFirstName(string firstName)
        {
            ServiceResponse<List<Contact>> serviceResponse = new ServiceResponse<List<Contact>>();

            if (!IsValidSearch(firstName))
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Invalid first name!";
                return serviceResponse;
            }

            var contacts = await this.bookRepository.GetContactsByFirstName(firstName);
            serviceResponse.Data = contacts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Contact>>> SearchByLastName(string lastName)
        {
            ServiceResponse<List<Contact>> serviceResponse = new ServiceResponse<List<Contact>>();

            if (!IsValidSearch(lastName))
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Invalid last name!";
                return serviceResponse;
            }

            var contacts = await this.bookRepository.GetContactsByLastName(lastName);
            serviceResponse.Data = contacts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Contact>>> SearchByPhoneNumber(string phoneNumber)
        {
            ServiceResponse<List<Contact>> serviceResponse = new ServiceResponse<List<Contact>>();

            if (!IsValidSearch(phoneNumber))
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Invalid phone number!";
                return serviceResponse;
            }

            var contacts = await this.bookRepository.GetContactsByPhoneNumber(phoneNumber);
            serviceResponse.Data = contacts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Contact>> Update(Contact contact)
        {
            ServiceResponse<Contact> serviceResponse = new ServiceResponse<Contact>();

            var contactDb = await this.bookRepository.GetContactById(contact.Id);
            var phoneNumberContactDb = await this.bookRepository.GetContactByPhoneNumber(contact.PhoneNumber);

            if (contactDb == null)
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "Contact not in the database!";
                return serviceResponse;
            }

            if (phoneNumberContactDb.Id != contactDb.Id)
            {
                serviceResponse.Successfull = false;
                serviceResponse.Message = "There is allread a contact with the same phone numner!";
                return serviceResponse;
            }


            await this.bookRepository.UpdateContact(contact);
            serviceResponse.Data = contactDb;
            return serviceResponse; ;
        }

        private bool IsValidSearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                return true;
            return false;
        }


        private bool AllFieldsCheck(Contact contact)
        {
            // Check if all fields have a value.
            // Manual check not a lot of properties
            if (contact == null)
                return false;

            if (!IsValidSearch(contact.FirstName) || !IsValidSearch(contact.LastName) || !IsValidSearch(contact.Address) || !IsValidSearch(contact.PhoneNumber))
                return false;

            return true;
        }

        private bool AllFieldsCheck2(Contact contact)
        {
            //TODO: Check if this is OK.
            // Check if all fields have a value not null.
            var type = typeof(Contact);
            foreach (var prop in type.GetProperties())
            {
                var val = prop.GetValue(contact, null);

                if (val == null)
                {
                    return false;
                }
            }
            return true;
        }




    }
}
