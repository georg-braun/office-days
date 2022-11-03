import { sameDay, addDays } from '../dateUtils';
let today = new Date();

export function getTodayMarker(currentDate) {	
    if (sameDay(today, currentDate)) {
        return "border-left: 2px solid  #000; border-right: 2px solid  #000;";
    }
}