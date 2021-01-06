import { EmptyReceipeGenerator } from './../services/receipes/EmptyReceipeGenerator';
import { EmptyReceipeGeneratorInterface } from '../abstract/receipes/EmptyReceipeGeneratorInterface';
import { ContainerModule, interfaces } from "inversify";


export const ReceipeModule = new ContainerModule((bind: interfaces.Bind) => {
    bind(nameof<EmptyReceipeGeneratorInterface>()).to(EmptyReceipeGenerator);
});