import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectCardComponent } from './project-card.component';

// eslint-disable-next-line no-undef
describe('ProjectCardComponent', () => {
    let component: ProjectCardComponent;
    let fixture: ComponentFixture<ProjectCardComponent>;

    // eslint-disable-next-line no-undef
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ProjectCardComponent],
        })
            .compileComponents();

        fixture = TestBed.createComponent(ProjectCardComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    // eslint-disable-next-line no-undef
    it('should create', () => {
        // eslint-disable-next-line no-undef
        expect(component).toBeTruthy();
    });
});
