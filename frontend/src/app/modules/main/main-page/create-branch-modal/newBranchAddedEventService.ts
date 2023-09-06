import { EventEmitter, Injectable } from '@angular/core';

import { BranchDto } from 'src/app/models/branch/branch-dto';

@Injectable({
    providedIn: 'root',
})
export class NewBranchAddedEventService {
    public newBranchAddedEventEmitter: EventEmitter<BranchDto> = new EventEmitter();

    emitNewBranchAddedEvent(data: BranchDto) {
        this.newBranchAddedEventEmitter.emit(data);
    }
}
