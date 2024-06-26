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
    /// EmbyNotificationsApiNotificationResult
    /// </summary>
    [DataContract]
        public partial class EmbyNotificationsApiNotificationResult :  IEquatable<EmbyNotificationsApiNotificationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbyNotificationsApiNotificationResult" /> class.
        /// </summary>
        /// <param name="notifications">notifications.</param>
        /// <param name="totalRecordCount">totalRecordCount.</param>
        public EmbyNotificationsApiNotificationResult(List<EmbyNotificationsApiNotification> notifications = default(List<EmbyNotificationsApiNotification>), int? totalRecordCount = default(int?))
        {
            this.Notifications = notifications;
            this.TotalRecordCount = totalRecordCount;
        }
        
        /// <summary>
        /// Gets or Sets Notifications
        /// </summary>
        [DataMember(Name="Notifications", EmitDefaultValue=false)]
        public List<EmbyNotificationsApiNotification> Notifications { get; set; }

        /// <summary>
        /// Gets or Sets TotalRecordCount
        /// </summary>
        [DataMember(Name="TotalRecordCount", EmitDefaultValue=false)]
        public int? TotalRecordCount { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EmbyNotificationsApiNotificationResult {\n");
            sb.Append("  Notifications: ").Append(Notifications).Append("\n");
            sb.Append("  TotalRecordCount: ").Append(TotalRecordCount).Append("\n");
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
            return this.Equals(input as EmbyNotificationsApiNotificationResult);
        }

        /// <summary>
        /// Returns true if EmbyNotificationsApiNotificationResult instances are equal
        /// </summary>
        /// <param name="input">Instance of EmbyNotificationsApiNotificationResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EmbyNotificationsApiNotificationResult input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Notifications == input.Notifications ||
                    this.Notifications != null &&
                    input.Notifications != null &&
                    this.Notifications.SequenceEqual(input.Notifications)
                ) && 
                (
                    this.TotalRecordCount == input.TotalRecordCount ||
                    (this.TotalRecordCount != null &&
                    this.TotalRecordCount.Equals(input.TotalRecordCount))
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
                if (this.Notifications != null)
                    hashCode = hashCode * 59 + this.Notifications.GetHashCode();
                if (this.TotalRecordCount != null)
                    hashCode = hashCode * 59 + this.TotalRecordCount.GetHashCode();
                return hashCode;
            }
        }

    }
}
