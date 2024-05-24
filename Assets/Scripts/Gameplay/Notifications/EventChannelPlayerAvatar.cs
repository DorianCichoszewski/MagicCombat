using MagicCombat.Gameplay.Player;
using Shared.Notification;
using UnityEngine;

namespace MagicCombat.Gameplay.Notifications
{
	[CreateAssetMenu(menuName = "Events/Player Avatar Event Channel")]
	public class EventChannelPlayerAvatar : EventChannel<PlayerAvatar> { }
}