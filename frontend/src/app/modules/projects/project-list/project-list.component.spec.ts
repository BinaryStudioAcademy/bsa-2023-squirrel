import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectListComponent } from './project-list.component';

// eslint-disable-next-line no-undef
describe('ProjectListComponent', () => {
    let component: ProjectListComponent;
    let fixture: ComponentFixture<ProjectListComponent>;

    // eslint-disable-next-line no-undef
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ProjectListComponent],
        })
            .compileComponents();

        fixture = TestBed.createComponent(ProjectListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    // eslint-disable-next-line no-undef
    it('should create', () => {
        // eslint-disable-next-line no-undef
        expect(component).toBeTruthy();
    });
});
