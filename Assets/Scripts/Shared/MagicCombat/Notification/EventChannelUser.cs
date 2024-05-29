using Shared.Data;
using UnityEngine;

namespace Shared.Notification
{
	[CreateAssetMenu(menuName = "Events/Player Event Channel")]
	public class EventChannelUser : EventChannel<UserId> { }
}