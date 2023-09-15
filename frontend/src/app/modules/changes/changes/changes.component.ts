import { Component } from '@angular/core';

import { TextPairDifferenceDto } from 'src/app/models/text-pair/text-pair-difference-dto';

@Component({
    selector: 'app-changes',
    templateUrl: './changes.component.html',
    styleUrls: ['./changes.component.sass'],
})
export class ChangesComponent {
    public textPair: TextPairDifferenceDto;

    constructor() {
        const json = `{
            "oldTextLines": [
              {
                "type": 0,
                "position": 1,
                "text": "The quick brown fox jumps over the lazy dog.",
                "subPieces": []
              },
              {
                "type": 1,
                "position": 2,
                "text": "A lazy brown dog is jumped over by the quick fox.",
                "subPieces": []
              },
              {
                "type": 0,
                "position": 3,
                "text": "An agile fox swiftly leaps past a lethargic dog.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 5,
                "text": "A fast fox gracefully clears the slumbering hound.",
                "subPieces": []
              }
            ],
            "newTextLines": [
              {
                "type": 0,
                "position": 1,
                "text": "The quick brown fox jumps over the lazy dog.",
                "subPieces": []
              },
              {
                "type": 3,
                "position": -1,
                "text": null,
                "subPieces": []
              },
              {
                "type": 0,
                "position": 2,
                "text": "An agile fox swiftly leaps past a lethargic dog.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 3,
                "text": "The white fox jumps over the indolent canine.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              },
              {
                "type": 4,
                "position": 4,
                "text": "A fox gracefully clears the slumbering hound.",
                "subPieces": []
              }             
            ],
            "hasDifferences": true
          }`;

        this.textPair = JSON.parse(json);
    }
}
