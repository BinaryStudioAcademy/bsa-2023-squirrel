import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsPageComponent } from './projects-page.component';

// eslint-disable-next-line no-undef
describe('ProjectsPageComponent', () => {
    let component: ProjectsPageComponent;
    let fixture: ComponentFixture<ProjectsPageComponent>;

    // eslint-disable-next-line no-undef
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ProjectsPageComponent],
        })
            .compileComponents();

        fixture = TestBed.createComponent(ProjectsPageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    // eslint-disable-next-line no-undef
    it('should create', () => {
        // eslint-disable-next-line no-undef
        expect(component).toBeTruthy();
    });
});
