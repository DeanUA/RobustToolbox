using System;
using NFluidsynth;
using Robust.Shared.Interfaces.Timing;
using Robust.Shared.IoC;
using Robust.Shared.Serialization;

namespace Robust.Shared.Audio.Midi
{
    /// <summary>
    ///     This class is a data representation of a Midi Event.
    ///     It's 'compatible' with NFluidsynth's own MidiEvent class.
    /// </summary>
    [Serializable, NetSerializable]
    public struct MidiEvent
    {
        public byte Type { get; set; }

        public byte Channel { get; set; }

        public byte Key { get; set; }

        public byte Velocity { get; set; }

        public byte Control { get; set; }

        public byte Value { get; set; }

        public byte Program { get; set; }

        public byte Pitch { get; set; }

        public uint Timestamp { get; set; }

        public static explicit operator MidiEvent(NFluidsynth.MidiEvent midiEvent)
        {
            return new MidiEvent()
            {
                Type = (byte) midiEvent.Type,
                Channel = (byte) midiEvent.Channel,
                Control = (byte) midiEvent.Control,
                Key = (byte) midiEvent.Key,
                Pitch = (byte) midiEvent.Pitch,
                Program = (byte) midiEvent.Program,
                Value = (byte) midiEvent.Value,
                Velocity = (byte) midiEvent.Velocity,
            };
        }

        public static implicit operator NFluidsynth.MidiEvent(MidiEvent midiEvent)
        {
            return new NFluidsynth.MidiEvent()
            {
                Type = midiEvent.Type,
                Channel = midiEvent.Channel,
                Control = midiEvent.Control,
                Key = midiEvent.Key,
                Pitch = midiEvent.Pitch,
                Program = midiEvent.Program,
                Value = midiEvent.Value,
                Velocity = midiEvent.Velocity,
            };
        }

        public static implicit operator NFluidsynth.SequencerEvent(MidiEvent midiEvent)
        {
            var @event = new NFluidsynth.SequencerEvent();

            switch (midiEvent.Type)
            {
                // NoteOff
                case 128:
                    @event.NoteOff(midiEvent.Channel, midiEvent.Key);
                    break;

                // NoteOn
                case 144:
                    if(midiEvent.Velocity != 0)
                        @event.NoteOn(midiEvent.Channel, midiEvent.Key, midiEvent.Velocity);
                    else
                        @event.NoteOff(midiEvent.Channel, midiEvent.Key);
                    break;

                // CC
                case 176:
                    @event.ControlChange(midiEvent.Channel, midiEvent.Control, midiEvent.Value);
                    break;

                // Pitch Bend
                case 224:
                    @event.PitchBend(midiEvent.Channel, midiEvent.Pitch);
                    break;
            }

            return @event;
        }


    }
}

