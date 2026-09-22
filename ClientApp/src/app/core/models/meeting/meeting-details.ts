import { Organizer } from "../user/organizer";

export interface MeetingDetails
{
    id: number;
    title: string;
    description: string;
    scheduledFor: Date;
    latitude: number;
    longitude: number;
    organizer: Organizer;
}
