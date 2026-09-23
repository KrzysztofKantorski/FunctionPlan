import { Organizer } from "../user/organizer";
import { MeetingParticipant } from "./meeting-participant";

export interface MeetingDetails
{
    id: number;
    title: string;
    description: string;
    scheduledFor: Date;
    latitude: number;
    longitude: number;
    organizer: Organizer;
    attendees: MeetingParticipant[];
}
