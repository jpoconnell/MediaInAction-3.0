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
    /// AuthenticateUser
    /// </summary>
    [DataContract]
        public partial class AuthenticateUser :  IEquatable<AuthenticateUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUser" /> class.
        /// </summary>
        /// <param name="pw">pw.</param>
        public AuthenticateUser(string pw = default(string))
        {
            this.Pw = pw;
        }
        
        /// <summary>
        /// Gets or Sets Pw
        /// </summary>
        [DataMember(Name="Pw", EmitDefaultValue=false)]
        public string Pw { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AuthenticateUser {\n");
            sb.Append("  Pw: ").Append(Pw).Append("\n");
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
            return this.Equals(input as AuthenticateUser);
        }

        /// <summary>
        /// Returns true if AuthenticateUser instances are equal
        /// </summary>
        /// <param name="input">Instance of AuthenticateUser to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AuthenticateUser input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Pw == input.Pw ||
                    (this.Pw != null &&
                    this.Pw.Equals(input.Pw))
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
                if (this.Pw != null)
                    hashCode = hashCode * 59 + this.Pw.GetHashCode();
                return hashCode;
            }
        }

    }
}
