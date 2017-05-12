using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class LocationUse
    {
        #region Properties
        /// <summary>
        /// Set instruction in all room in the house
        /// </summary>
        private bool _house = false;
        public bool House
        {
            get
            {
                return _house;
            }

            set
            {
                if (value)
                {
                    this.LivingRoom = true;
                    this.Kitchen = true;
                    this.ParentBedroom = true;
                    this.KidBedRoom = true;
                    this.Toilet = true;
                    this.BathRoom = true;
                    //this.BathRoom.All(BathRoom => BathRoom = true);
                    //this.BedRoom.All(BedRoom => BedRoom = true);
                }

                _house = value;
            }
        }

        /// <summary>
        /// Set instruction in the LivingRoom
        /// </summary>
        public bool LivingRoom { get; set; }

        /// <summary>
        /// Set instruction in the Kitchen
        /// </summary>
        public bool Kitchen { get; set; }

        /// <summary>
        /// Set instruction in the BedRoom
        /// </summary>
        public bool ParentBedroom { get; set; }

        /// <summary>
        /// Set instruction in the BedRoom
        /// </summary>
        public bool KidBedRoom { get; set; }

        /// <summary>
        /// Set instruction in the BedRoom
        /// </summary>
        public bool Desk { get; set; }

        /// <summary>
        /// Set instruction in the BedRoom
        /// </summary>
        public bool Toilet { get; set; }

        /// <summary>
        /// Set instruction in the BathRoom
        /// </summary>
        public bool BathRoom { get; set; }
        #endregion
    }
}
