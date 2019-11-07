using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_NTier_XmlJsonData.Models;
using Demo_NTier_XmlJsonData.DataAccessLayer;

namespace Demo_NTier_XmlJsonData.BusinessLayer
{
    public enum FileIoMessage
    {
        None,
        Complete,
        FileAccessError,
        RecordNotFound
    }

    public class FlintstoneCharacterBusiness
    {
        public FileIoMessage FileIoStatus { get; set; }

        public FlintstoneCharacterBusiness()
        {

        }

        /// <summary>
        /// retrieve a list of all characters
        /// </summary>
        /// <returns>all characters</returns>
        private List<FlintstoneCharacter> FlintstoneCharactersList()
        {
            List<FlintstoneCharacter> characters = new List<FlintstoneCharacter>();
            FileIoStatus = FileIoMessage.None;

            using (FlintstoneCharacterRepository fsRepository = new FlintstoneCharacterRepository())
            {
                try
                {
                    characters = fsRepository.GetAll() as List<FlintstoneCharacter>;
                    FileIoStatus = FileIoMessage.Complete;
                }
                catch (Exception)
                {
                    FileIoStatus = FileIoMessage.FileAccessError;
                }
            };

            return characters;
        }

        /// <summary>
        /// retrieve a list of all characters
        /// </summary>
        /// <returns>list of all characters</returns>
        public List<FlintstoneCharacter> AllFlintstoneCharacters()
        {
            return FlintstoneCharactersList() as List<FlintstoneCharacter>;
        }

        /// <summary>
        /// retrieve a character by id
        /// </summary>
        /// <param name="id">character id</param>
        /// <returns>character</returns>
        public FlintstoneCharacter FlintstoneCharacterById(int id)
        {
            List<FlintstoneCharacter> characters = FlintstoneCharactersList();
            FlintstoneCharacter character = characters.FirstOrDefault(c => c.Id == id);

            if (character == null)
            {
                FileIoStatus = FileIoMessage.RecordNotFound;
            }
            else
            {
                FileIoStatus = FileIoMessage.Complete;
            }

            return character;
        }

        /// <summary>
        /// Add a character
        /// </summary>
        /// <param name="character"></param>
        public void AddFlintstoneCharacter(FlintstoneCharacter character)
        {
            FileIoStatus = FileIoMessage.None;

            using (FlintstoneCharacterRepository fsRepository = new FlintstoneCharacterRepository())
            {
                try
                {
                    fsRepository.Add(character);
                    FileIoStatus = FileIoMessage.Complete;
                }
                catch (Exception)
                {
                    FileIoStatus = FileIoMessage.FileAccessError;
                }
            }
        }
        /// <summary>
        /// Delete a character
        /// </summary>
        /// <param name="character"></param>
        public void DeleteFlintstoneCharacter(FlintstoneCharacter character)
        {
            FileIoStatus = FileIoMessage.None;

            using (FlintstoneCharacterRepository fsRepository = new FlintstoneCharacterRepository())
            {
                try
                {
                    fsRepository.Delete(character.Id);
                    FileIoStatus = FileIoMessage.Complete;
                }
                catch (Exception)
                {
                    FileIoStatus = FileIoMessage.FileAccessError;
                }
            }
        }
    }
}
