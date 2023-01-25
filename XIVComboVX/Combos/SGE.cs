namespace XIVComboVX.Combos;

using Dalamud.Game.ClientState.JobGauge.Types;

using XIVComboVX;

internal static class SGE {
	public const byte JobID = 40;

	public const uint
		Dosis = 24283,
		Diagnosis = 24284,
		Kardia = 24285,
		Prognosis = 24286,
		Egeiro = 24287,
		Physis = 24288,
		Phlegma = 24289,
		Eukrasia = 24290,
		Soteria = 24294,
		Icarus = 24295,
		Druochole = 24296,
		Dyskrasia = 24297,
		Kerachole = 24298,
		Ixochole = 24299,
		Zoe = 24300,
		Pepsis = 24301,
		Physis2 = 24302,
		Taurochole = 24303,
		Toxikon = 24304,
		Haima = 24305,
		Phlegma2 = 24307,
		Rhizomata = 24309,
		Holos = 24310,
		Panhaima = 24311,
		Phlegma3 = 24313,
		Krasis = 24317,
		Pneuma = 24318;

	public static class Buffs {
		public const ushort
			Kardion = 2604;
	}

	public static class Debuffs {
		public const ushort
			Placeholder = 0;
	}

	public static class Levels {
		public const ushort
			Dosis = 1,
			Prognosis = 10,
			Egeiro = 12,
			Phlegma = 26,
			Soteria = 35,
			Icarus = 40,
			Druochole = 45,
			Dyskrasia = 46,
			Kerachole = 50,
			Ixochole = 52,
			Physis2 = 60,
			Taurochole = 62,
			Toxikon = 66,
			Haima = 70,
			Phlegma2 = 72,
			Dosis2 = 72,
			Rhizomata = 74,
			Holos = 76,
			Panhaima = 80,
			Phlegma3 = 82,
			Dosis3 = 82,
			Krasis = 86,
			Pneuma = 90;
	}
}

internal class SageSwiftcastRaiserFeature: SwiftRaiseCombo {
	public override CustomComboPreset Preset => CustomComboPreset.SageSwiftcastRaiserFeature;
	public override uint[] ActionIDs { get; } = new[] { SGE.Egeiro };
}

internal class SageSoteria: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageSoteriaKardionFeature;
	public override uint[] ActionIDs => new[] { SGE.Soteria };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (SelfHasEffect(SGE.Buffs.Kardion))
			return SGE.Soteria;

		return SGE.Kardia;
	}
}

internal class SageTaurochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	public override uint[] ActionIDs => new[] { SGE.Taurochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SageTaurocholeRhizomataFeature)) {
			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;
		}

		if (IsEnabled(CustomComboPreset.SageTaurocholeDruocholeFeature)) {
			if (level >= SGE.Levels.Taurochole && IsOffCooldown(SGE.Taurochole))
				return SGE.Taurochole;

			return SGE.Druochole;
		}

		return actionID;
	}
}

internal class SageDruochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageDruocholeRhizomataFeature;
	public override uint[] ActionIDs => new[] { SGE.Druochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
			return SGE.Rhizomata;

		return actionID;
	}
}

internal class SageIxochole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageIxocholeRhizomataFeature;
	public override uint[] ActionIDs => new[] { SGE.Ixochole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
			return SGE.Rhizomata;

		return actionID;
	}
}

internal class SageKerachole: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	public override uint[] ActionIDs => new[] { SGE.Kerachole };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (IsEnabled(CustomComboPreset.SageKeracholaRhizomataFeature)) {
			if (level >= SGE.Levels.Rhizomata && GetJobGauge<SGEGauge>().Addersgall == 0)
				return SGE.Rhizomata;
		}

		if (IsEnabled(CustomComboPreset.SageKeracholeHolos)) {
			if (level >= SGE.Levels.Holos) {
				if (GetJobGauge<SGEGauge>().Addersgall == 0)
					return PickByCooldown(SGE.Holos, SGE.Holos, SGE.Kerachole);
			}
		}

		return actionID;
	}
}

internal class SageHolos: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageHolosKerachole;
	public override uint[] ActionIDs => new[] { SGE.Holos };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level < SGE.Levels.Holos)
			return SGE.Kerachole;

		if (GetJobGauge<SGEGauge>().Addersgall > 0)
			return PickByCooldown(SGE.Holos, SGE.Holos, SGE.Kerachole);

		return actionID;
	}
}

internal class SagePhlegma: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SgeAny;
	public override uint[] ActionIDs => new[] { SGE.Phlegma, SGE.Phlegma2, SGE.Phlegma3 };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {
		uint phlegma = OriginalHook(actionID);

		// Phlegma, Icarus, Toxikon, and Dosis are all targeted actions, so if you don't have a target, we won't bother checking for any of them
		if (HasTarget) {

			// First, if you have a target in range for Phlegma and it's usable, we just do that
			if (TargetDistance <= 6 && CanUse(phlegma)) {

				if (IsEnabled(CustomComboPreset.SageLucidPhlegma) && level >= Common.Levels.LucidDreaming) {
					if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidPhlegmaManaThreshold) {
						if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
							return Common.LucidDreaming;
					}
				}

				return phlegma;
			}
			// Fallthrough: target out of range OR phlegma down

			// Prioritise Icarus into Phlegma over Toxikon because Phlegma is higher potency
			if (IsEnabled(CustomComboPreset.SagePhlegmaIcarus) && level >= SGE.Levels.Icarus) {
				if (TargetDistance > Service.Configuration.SagePhlegmaIcarusDistanceThreshold) {
					if (CanUse(SGE.Icarus) && CanUse(phlegma))
						return SGE.Icarus;
				}
			}
			// Fallthrough: target not far enough OR icarus down OR phlegma down

			// Even if you CAN use Phlegma, if you're too far away and Icarus isn't up, fall back to the lower-potency Toxikon cause it's a target-AOE too
			if (IsEnabled(CustomComboPreset.SagePhlegmaToxicon) && level >= SGE.Levels.Toxikon) {
				if (GetJobGauge<SGEGauge>().Addersting > 0 && (!CanUse(phlegma) || TargetDistance > 6)) {

					if (IsEnabled(CustomComboPreset.SageLucidToxikon) && level >= Common.Levels.LucidDreaming) {
						if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidToxikonManaThreshold) {
							if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
								return Common.LucidDreaming;
						}
					}

					return OriginalHook(SGE.Toxikon);
				}
			}
			// Fallthrough: no addersting AND ((target out of range AND not far enough) OR phlegma down)

			// Prioritise Dosis over Dyskrasia if you have a target in range because it's much higher potency and a targeted attack
			// It's actually higher potency than Toxikon too, but this is a fallback because the button is MEANT for target-AOE attacks and Dosis is ST
			// Note that this DOESN'T check for Phlegma being down! If it's up but we got here anyway, then the target must be out of range, so Dyskrasia wouldn't hit them either.
			// If you want to use Dyskrasia over Dosis, clear your target with escape or clicking on the ground.
			if (IsEnabled(CustomComboPreset.SagePhlegmaDosis)) {
				if (TargetDistance <= 25) {

					if (IsEnabled(CustomComboPreset.SageLucidDosis) && level >= Common.Levels.LucidDreaming) {
						if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidDosisManaThreshold) {
							if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
								return Common.LucidDreaming;
						}
					}

					return OriginalHook(SGE.Dosis);
				}
			}
			// Fallthrough: no addersting AND target out of range (incl. for icarus)
		}
		// Fallthrough: (no addersting AND target out of range (incl. for icarus)) OR no target

		// Fall back to Dyskrasia if there's nothing else we can do, because you'd have to be FAIRLY close for Phlegma so hopefully this self-AOE will still hit enough to be worth it
		// This also triggers if you HAVE a target, but they're out of range for Dosis/Icarus (or you just don't have that stuff enabled)
		if (IsEnabled(CustomComboPreset.SagePhlegmaDyskrasia) && level >= SGE.Levels.Dyskrasia) {

			if (IsEnabled(CustomComboPreset.SageLucidDyskrasia) && level >= Common.Levels.LucidDreaming) {
				if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidDyskrasiaManaThreshold) {
					if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
						return Common.LucidDreaming;
				}
			}

			return OriginalHook(SGE.Dyskrasia);
		}
		// Fallthrough: none (excluding level too low or combos not enabled)

		return actionID;
	}
}

internal class SageDosis: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageLucidDosis;
	public override uint[] ActionIDs => new[] { SGE.Dosis };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= Common.Levels.LucidDreaming) {
			if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidDosisManaThreshold) {
				if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
					return Common.LucidDreaming;
			}
		}

		return actionID;
	}
}

internal class SageDyskrasia: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageLucidDyskrasia;
	public override uint[] ActionIDs => new[] { SGE.Dyskrasia };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= Common.Levels.LucidDreaming) {
			if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidDyskrasiaManaThreshold) {
				if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
					return Common.LucidDreaming;
			}
		}

		return actionID;
	}
}

internal class SageToxikon: CustomCombo {
	public override CustomComboPreset Preset { get; } = CustomComboPreset.SageLucidToxikon;
	public override uint[] ActionIDs => new[] { SGE.Toxikon };

	protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level) {

		if (level >= Common.Levels.LucidDreaming) {
			if (LocalPlayer.CurrentMp < Service.Configuration.SageLucidToxikonManaThreshold) {
				if (CanUse(Common.LucidDreaming) && CanWeave(actionID))
					return Common.LucidDreaming;
			}
		}

		return actionID;
	}
}
