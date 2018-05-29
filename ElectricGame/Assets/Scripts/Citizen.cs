using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CitizenJob {Doctor=0, SportsPlayer, Teacher, ConstructionWorker, Musician, Fireman, Butcher, OfficeWorker, Student, Baker, Policeman, Lawyer, Pilot, Soldier}

public class Citizen : Entity {
	public int age {get; protected set;}
	public int happiness {get; protected set;}
	public CitizenJob job  {get; protected set;}

	public Citizen(string name = "Bob", int age = 18, CitizenJob job = CitizenJob.Doctor) : base(name) {
        this.age = age;
		this.job = job;
        happiness = 100;
    }
}
