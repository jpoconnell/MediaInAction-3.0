/*
 * EmbyClient.Dotnet
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwaggerDateConverter = EmbyClient.Dotnet.Client.SwaggerDateConverter;

namespace EmbyClient.Dotnet.Model
{
    /// <summary>
    /// NetEndPointInfo
    /// </summary>
    [DataContract]
        public partial class NetEndPointInfo :  IEquatable<NetEndPointInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetEndPointInfo" /> class.
        /// </summary>
        /// <param name="isLocal">isLocal.</param>
        /// <param name="isInNetwork">isInNetwork.</param>
        public NetEndPointInfo(bool? isLocal = default(bool?), bool? isInNetwork = default(bool?))
        {
            this.IsLocal = isLocal;
            this.IsInNetwork = isInNetwork;
        }
        
        /// <summary>
        /// Gets or Sets IsLocal
        /// </summary>
        [DataMember(Name="IsLocal", EmitDefaultValue=false)]
        public bool? IsLocal { get; set; }

        /// <summary>
        /// Gets or Sets IsInNetwork
        /// </summary>
        [DataMember(Name="IsInNetwork", EmitDefaultValue=false)]
        public bool? IsInNetwork { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class NetEndPointInfo {\n");
            sb.Append("  IsLocal: ").Append(IsLocal).Append("\n");
            sb.Append("  IsInNetwork: ").Append(IsInNetwork).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as NetEndPointInfo);
        }

        /// <summary>
        /// Returns true if NetEndPointInfo instances are equal
        /// </summary>
        /// <param name="input">Instance of NetEndPointInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NetEndPointInfo input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.IsLocal == input.IsLocal ||
                    (this.IsLocal != null &&
                    this.IsLocal.Equals(input.IsLocal))
                ) && 
                (
                    this.IsInNetwork == input.IsInNetwork ||
                    (this.IsInNetwork != null &&
                    this.IsInNetwork.Equals(input.IsInNetwork))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.IsLocal != null)
                    hashCode = hashCode * 59 + this.IsLocal.GetHashCode();
                if (this.IsInNetwork != null)
                    hashCode = hashCode * 59 + this.IsInNetwork.GetHashCode();
                return hashCode;
            }
        }

    }
}
