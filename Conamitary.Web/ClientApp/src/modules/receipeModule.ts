import { ReceipesPaginationService } from './../services/receipes/ReceipesPaginationService';
import { ReceipesPaginationServiceInterface } from './../abstract/receipes/ReceipesPaginationServiceInterface';
import { EmptyReceipeGenerator } from './../services/receipes/EmptyReceipeGenerator';
import { EmptyReceipeGeneratorInterface } from '../abstract/receipes/EmptyReceipeGeneratorInterface';
import { ContainerModule, interfaces } from 'inversify';

export const ReceipeModule = new ContainerModule((bind: interfaces.Bind) => {
    bind(nameof<EmptyReceipeGeneratorInterface>()).to(EmptyReceipeGenerator);
    bind(nameof<ReceipesPaginationServiceInterface>()).to(
        ReceipesPaginationService
    );
});
