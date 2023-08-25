import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateProjectModalComponent } from './create-project-modal.component';

// eslint-disable-next-line no-undef
describe('CreateProjectModalComponent', () => {
    let component: CreateProjectModalComponent;
    let fixture: ComponentFixture<CreateProjectModalComponent>;

    // eslint-disable-next-line no-undef
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [CreateProjectModalComponent],
        })
            .compileComponents();

        fixture = TestBed.createComponent(CreateProjectModalComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    // eslint-disable-next-line no-undef
    it('should create', () => {
        // eslint-disable-next-line no-undef
        expect(component).toBeTruthy();
    });
});
