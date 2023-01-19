using AddressBook_API.Interfaces;
using AddressBook_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook_API.Data.Repository
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly DataContext dataContext;

        public AddressBookRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            this.dataContext.Contacts.Add(contact);
            await this.dataContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> DeleteContact(int contactId)
        {
            var contactToRemove = await dataContext.Contacts.Where(c => c.Id == contactId).FirstOrDefaultAsync();
            if (contactToRemove != null)
            {
                this.dataContext.Contacts.Remove(contactToRemove);
                await this.dataContext.SaveChangesAsync();
                return contactToRemove;
            }
            return null;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await this.dataContext.Contacts.ToListAsync();
        }

        public async Task<List<Contact>> GetContacts(int page, int contactsPerPage)
        {
            return await this.dataContext.Contacts
                .Skip(page * contactsPerPage)
                .Take(contactsPerPage)
                .ToListAsync();
        }

        public async Task<Contact?> GetContactByPhoneNumber(string phoneNumber)
        {
            return await this.dataContext.Contacts.Where(c => c.PhoneNumber.ToLower() == phoneNumber.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetContactsByAddress(string address)
        {
            return await this.dataContext.Contacts.Where(c => c.Address.Contains(address)).ToListAsync();
        }

        public async Task<List<Contact>> GetContactsByFirstName(string firstName)
        {
            return await this.dataContext.Contacts.Where(c => c.LastName.Contains(firstName)).ToListAsync();
        }

        public async Task<List<Contact>> GetContactsByLastName(string lastName)
        {
            return await this.dataContext.Contacts.Where(c => c.LastName.Contains(lastName)).ToListAsync();
        }

        public async Task<List<Contact>> GetContactsByPhoneNumber(string phoneNumber)
        {
            return await this.dataContext.Contacts.Where(c => c.PhoneNumber.Contains(phoneNumber)).ToListAsync();
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            var contactDb = await GetContactById(contact.Id);
            if (contactDb != null)
            {
                contactDb.FirstName = contact.FirstName;
                contactDb.LastName = contact.LastName;
                contactDb.Address = contact.Address;
                contactDb.PhoneNumber = contact.PhoneNumber;

                await this.dataContext.SaveChangesAsync();
            }
            return contactDb;
        }


        public async Task<Contact?> GetContactById(int contactId)
        {
            return await this.dataContext.Contacts.Where(c => c.Id == contactId).FirstOrDefaultAsync();
        }
    }
}
